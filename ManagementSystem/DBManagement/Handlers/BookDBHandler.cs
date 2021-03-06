﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using CoreLibrary.Inventory;
using CoreLibrary.Members;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Data.SqlTypes;

namespace CoreLibrary.DBManagement.Handlers
{
	class BookDBHandler : IBookDBHandler {
		Assembly assembly;

		private const string ADD_NEW_BOOK_QUERY_RESOURCE = "BookQueries.AddNewBook.sql";
		private const string BOOK_AVAILABILITY_RESOURCE = "BookQueries.BookAvailability.sql";
		private const string CHECKOUT_BOOK_RESOURCE = "BookQueries.CheckoutBook.sql";
		private const string RETURN_BOOK_RESOURCE = "BookQueries.ReturnBook.sql";
		private const string GET_FORMAT_RESOURCE = "BookQueries.GetFormat.sql";
		private const string GET_BOOK_BY_ID_RESOURCE = "BookQueries.GetBookByID.sql";
		private const string GET_NEW_LIBRARY_ID_RESOURCE = "BookQueries.GetNewLibraryID.sql";
		private const string UPDATE_BOOK_RESOURCE = "BookQueries.UpdateBook.sql";
		private const string REMOVE_BOOK_RESOURCE = "BookQueries.RemoveBook.sql";

		private BookDBHandler() {
			assembly = Assembly.GetExecutingAssembly();
			// Prevent instantiation
		}

		internal static IBookDBHandler NewInstance() {
			return new BookDBHandler();
		}



		private IEnumerable<IBook> GetBooksByCommand(SqlCommand command) {
			// Container for the queried books
			List<IBook> items = new List<IBook>();
			// Open connection to the database
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				command.Connection = connection;
				connection.Open();
				// Initiate a data reader via the command object
				using (command)
				using (SqlDataReader reader = command.ExecuteReader()) {
					// Read all the data from reader and put the info into a book
					while (reader.Read()) {
						IBook book = new Book {
							LibraryID = reader.GetInt32(reader.GetOrdinal("Library_ID")),
							Title = reader.GetString(reader.GetOrdinal("Title")),
							Author = reader.GetString(reader.GetOrdinal("Author")),
							ISBN10 = reader.GetString(reader.GetOrdinal("ISBN10")),
							ISBN13 = reader.GetString(reader.GetOrdinal("ISBN13")),
							Length = reader.GetInt32(reader.GetOrdinal("Length")),
							Genre = reader.GetString(reader.GetOrdinal("Genre")),
							Publisher = reader.GetString(reader.GetOrdinal("Publisher")),
							PublicationYear = reader.GetInt32(reader.GetOrdinal("Publication_Year")),
							Description = reader.GetString(reader.GetOrdinal("Description")),
							InStock = reader.GetBoolean(reader.GetOrdinal("In_Stock"))
						};
						int mediaKey = reader.GetInt32(reader.GetOrdinal("Format"));
						string mediaFormat = MediaFormat.GetMediaFromKey(mediaKey);
						book.Format = mediaFormat;
						book.ImageBytes = InventoryManager.GetItemImage(book.LibraryID);
						items.Add(book);
						}
					}
				// Possibly unnecessary. When in doubt, be explicit
				connection.Close();
			}

			return items;
		}

		public bool AddNewBook(IBook book) {			
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				// Add a new row to the Inventory_Books table
				// Update the number of copies in Inventory_Master
				using (SqlCommand command = new SqlCommand("AddNewBook", connection)) {
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@libraryID", book.LibraryID);
					command.Parameters.AddWithValue("@title", book.Title);
					command.Parameters.AddWithValue("@author", book.Author);
					command.Parameters.AddWithValue("@format", MediaFormat.GetMediaKey(book.Format));
					command.Parameters.AddWithValue("@isbn10", book.ISBN10);
					command.Parameters.AddWithValue("@isbn13", book.ISBN13);
					command.Parameters.AddWithValue("@length", book.Length);
					command.Parameters.AddWithValue("@genre", book.Genre);
					command.Parameters.AddWithValue("@publisher", book.Publisher);
					command.Parameters.AddWithValue("@publicationYear", book.PublicationYear);
					command.Parameters.AddWithValue("@description", book.Description);
					if (book.ImageBytes == null) {
						command.Parameters.AddWithValue("@image", DBNull.Value);
					} else {
						command.Parameters.AddWithValue("@image", book.ImageBytes);
					}
					
					command.ExecuteNonQuery();
				}
			}

			return true;
		}

		public void UpdateBook(IBook book) {
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				// Add a new row to the Inventory_Books table
				// Update the number of copies in Inventory_Master
				using (SqlCommand command = new SqlCommand("UpdateBook", connection)) {
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@libraryID", book.LibraryID);
					command.Parameters.AddWithValue("@title", book.Title);
					command.Parameters.AddWithValue("@author", book.Author);
					int mediaKey = MediaFormat.GetMediaKey(book.Format);
					command.Parameters.AddWithValue("@format", mediaKey);
					command.Parameters.AddWithValue("@isbn10", book.ISBN10);
					command.Parameters.AddWithValue("@isbn13", book.ISBN13);
					command.Parameters.AddWithValue("@length", book.Length);
					command.Parameters.AddWithValue("@genre", book.Genre);
					command.Parameters.AddWithValue("@publisher", book.Publisher);
					command.Parameters.AddWithValue("@publicationYear", book.PublicationYear);
					command.Parameters.AddWithValue("@description", book.Description);
					if (book.ImageBytes == null) {
						command.Parameters.AddWithValue("@image", DBNull.Value);
					} else {
						command.Parameters.AddWithValue("@image", book.ImageBytes);
					}
					command.ExecuteNonQuery();
				}
			}
		}

		public int BookAvailability(IBook book) {
			int copies = 0;			
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				// Initiate a data reader via the command object
				using (SqlCommand command = new SqlCommand(DBManager.GetQueryTextFromResource(BOOK_AVAILABILITY_RESOURCE), connection))
				using (SqlDataReader reader = command.ExecuteReader()) {
					// Read all the data from reader and put the info into a book
					while (reader.Read()) {
						copies = reader.GetInt16(reader.GetOrdinal("Copies_In_Stock"));
					}
				}
				// Possibly unnecessary. When in doubt, be explicit
				connection.Close();
			}

			return copies;
		}

		public bool CheckoutBook(IBook book, IAccount member) {
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();

				using (SqlCommand command = new SqlCommand(DBManager.GetQueryTextFromResource(CHECKOUT_BOOK_RESOURCE), connection)) {
					command.Parameters.AddWithValue("@libraryID", book.LibraryID);
					command.Parameters.AddWithValue("@checkoutDate", SqlDateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
					command.Parameters.AddWithValue("@dueDate", DateTime.Today.AddDays(book.LengthOfLoan));
					command.Parameters.AddWithValue("@mediaType", MediaFormat.GetMediaKey(book.Format));
					command.ExecuteNonQuery();
				}

				// Possibly unnecessary. When in doubt, be explicit
				connection.Close();
			}

			return true;
		}

		public bool ReturnBook(IBook book) {
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();

				using (SqlCommand command = new SqlCommand(StoredProcedures.RETURN_BOOK, connection)) {
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@libraryID", book.LibraryID);
					command.ExecuteNonQuery();
				}

				// Possibly unnecessary. When in doubt, be explicit
				connection.Close();
			}

			return true;
		}

		public IBook GetBookByLibraryID(int ID) {
			SqlCommand command = new SqlCommand(DBManager.GetQueryTextFromResource(GET_BOOK_BY_ID_RESOURCE));
			command.Parameters.AddWithValue("@libraryID", ID);
			List<IBook> queryResult = this.GetBooksByCommand(command).ToList();

			return queryResult[0];
		}

		public IEnumerable<IBook> GetBooks() {
			SqlCommand command = new SqlCommand(
				"SELECT * " +
				"FROM Inventory_Books",
				DBManager.GetSqlConnection());

			return this.GetBooksByCommand(command);
		}

		public IEnumerable<IBook> GetBooksByAuthor(string author) {
			SqlCommand command = new SqlCommand(
			"SELECT * " +
			"FROM Inventory_Books" +
			$"WHERE Author={author}",
			DBManager.GetSqlConnection());

			return this.GetBooksByCommand(command);
		}

		public IEnumerable<IBook> GetBooksByGenre(string genre) {
			SqlCommand command = new SqlCommand(
			"SELECT * " +
			"FROM Inventory_Books" +
			$"WHERE Genre={genre}",
			DBManager.GetSqlConnection());

			return this.GetBooksByCommand(command);
		}

		public IEnumerable<IBook> GetBooksByISBN(string ISBN) {
			SqlCommand command;
			if (ISBN.Length == 10) {
				command = new SqlCommand(
				"SELECT * " +
				"FROM Inventory_Books" +
				$"WHERE ISBN10={ISBN}",
				DBManager.GetSqlConnection());
			} else {
				command = new SqlCommand(
				"SELECT * " +
				"FROM Inventory_Books" +
				$"WHERE ISBN13={ISBN}",
				DBManager.GetSqlConnection());
			}

			return this.GetBooksByCommand(command);
		}

		public IEnumerable<IBook> GetBooksByPublisher(string publisher) {
			SqlCommand command = new SqlCommand(
			"SELECT * " +
			"FROM Inventory_Books" +
			$"WHERE Publisher={publisher}",
			DBManager.GetSqlConnection());

			return this.GetBooksByCommand(command);
		}

		public IEnumerable<IBook> GetBooksByTitle(string title) {
			SqlCommand command = new SqlCommand(
			"SELECT * " +
			"FROM Inventory_Books" +
			$"WHERE Title={title}",
			DBManager.GetSqlConnection());

			return this.GetBooksByCommand(command);
		}

		public IEnumerable<IBook> GetCheckedOutBooks() {
			// Container for the queried books
			List<IBook> items = new List<IBook>();
			// Open connection to the database
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				// Initiate a data reader via the command object
				using (SqlCommand command = new SqlCommand(StoredProcedures.GET_CHECKED_OUT_BOOKS, connection)) {
					command.CommandType = System.Data.CommandType.StoredProcedure;
					using (SqlDataReader reader = command.ExecuteReader()) {
						// Read all the data from reader and put the info into a book
						while (reader.Read()) {
							IBook book = new Book {
								LibraryID = reader.GetInt32(reader.GetOrdinal("Library_ID")),
								Title = reader.GetString(reader.GetOrdinal("Title")),
								Author = reader.GetString(reader.GetOrdinal("Author")),
								ISBN10 = reader.GetString(reader.GetOrdinal("ISBN10")),
								ISBN13 = reader.GetString(reader.GetOrdinal("ISBN13")),
								Length = reader.GetInt32(reader.GetOrdinal("Length")),
								Genre = reader.GetString(reader.GetOrdinal("Genre")),
								Publisher = reader.GetString(reader.GetOrdinal("Publisher")),
								PublicationYear = reader.GetInt32(reader.GetOrdinal("Publication_Year")),
								Description = reader.GetString(reader.GetOrdinal("Description")),
								InStock = reader.GetBoolean(reader.GetOrdinal("In_Stock"))
							};
							int mediaKey = reader.GetInt32(reader.GetOrdinal("Format"));
							string mediaFormat = MediaFormat.GetMediaFromKey(mediaKey);
							book.Format = mediaFormat;
							book.ImageBytes = InventoryManager.GetItemImage(book.LibraryID);
							items.Add(book);
						}
					}
					// Possibly unnecessary. When in doubt, be explicit
					connection.Close();
				}
			}

			return items;
		}

		public IEnumerable<IBook> GetCheckedOutBooksByUser(string email) {
			// Container for the queried books
			List<IBook> items = new List<IBook>();
			// Open connection to the database
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				// Initiate a data reader via the command object
				using (SqlCommand command = new SqlCommand(StoredProcedures.GET_CHECKED_OUT_BOOKS_BY_USER, connection)) {
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@email", email);
					using (SqlDataReader reader = command.ExecuteReader()) {
						// Read all the data from reader and put the info into a book
						while (reader.Read()) {
							IBook book = new Book {
								LibraryID = reader.GetInt32(reader.GetOrdinal("Library_ID")),
								Title = reader.GetString(reader.GetOrdinal("Title")),
								Author = reader.GetString(reader.GetOrdinal("Author")),
								ISBN10 = reader.GetString(reader.GetOrdinal("ISBN10")),
								ISBN13 = reader.GetString(reader.GetOrdinal("ISBN13")),
								Length = reader.GetInt32(reader.GetOrdinal("Length")),
								Genre = reader.GetString(reader.GetOrdinal("Genre")),
								Publisher = reader.GetString(reader.GetOrdinal("Publisher")),
								PublicationYear = reader.GetInt32(reader.GetOrdinal("Publication_Year")),
								Description = reader.GetString(reader.GetOrdinal("Description")),
								InStock = reader.GetBoolean(reader.GetOrdinal("In_Stock"))
							};
							int mediaKey = reader.GetInt32(reader.GetOrdinal("Format"));
							string mediaFormat = MediaFormat.GetMediaFromKey(mediaKey);
							book.Format = mediaFormat;
							book.ImageBytes = InventoryManager.GetItemImage(book.LibraryID);
							items.Add(book);
						}
					}
					// Possibly unnecessary. When in doubt, be explicit
					connection.Close();
				}
			}

			return items;
		}

		public void RemoveBook(IBook book) {
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				// Add a new row to the Inventory_Books table
				// Update the number of copies in Inventory_Master
				using (SqlCommand command = new SqlCommand(DBManager.GetQueryTextFromResource(REMOVE_BOOK_RESOURCE), connection)) {
					command.Parameters.AddWithValue("@libraryID", book.LibraryID);
					command.ExecuteNonQuery();
				}
			}
		}

		public DateTime GetDueDate(int libraryID) {
			DateTime dueDate = DateTime.Today;
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				// Initiate a data reader via the command object
				using (SqlCommand command = new SqlCommand(StoredProcedures.GET_DUE_DATE, connection)) {
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@libraryID", libraryID);
					using (SqlDataReader reader = command.ExecuteReader()) {
						// Read all the data from reader and put the info into a book
						while (reader.Read()) {
							dueDate = reader.GetDateTime(0);
						}
					}
				}
				
				// Possibly unnecessary. When in doubt, be explicit
				connection.Close();
			}

			return dueDate;
		}
	}
}
