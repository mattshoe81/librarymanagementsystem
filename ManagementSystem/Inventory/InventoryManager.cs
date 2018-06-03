using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CoreLibrary.Inventory
{
    public static class InventoryManager
    {
		public static void AddNewBook(IBook book) {
			DBManagement.DBManager.NewBookDBHandler().AddNewBook(book);
		}

		public static int GetNewLibraryID() {
			int libraryID = 0;
			using (SqlConnection connection = DBManagement.DBManager.GetSqlConnection()) {
				connection.Open();

				using (SqlCommand command = new SqlCommand(DBManagement.DBManager.GetQueryTextFromResource("General.GetNewLibraryID.sql"), connection)) 
				using (SqlDataReader reader = command.ExecuteReader()) {

					reader.Read();
					libraryID = reader.GetInt32(0);

				}
			}

			return libraryID;
		}

		public static void UpdateBook(IBook book) {
			DBManagement.DBManager.NewBookDBHandler().UpdateBook(book);
		}

		public static bool IsValidLibraryID(int libraryID) {
			int count = 0;
			using (SqlConnection connection = DBManagement.DBManager.GetSqlConnection()) {
				connection.Open();

				using (SqlCommand command = new SqlCommand(DBManagement.DBManager.GetQueryTextFromResource("General.CheckLibraryIDExists.sql"), connection)) { 
					command.Parameters.AddWithValue("@libraryID", libraryID);

					using (SqlDataReader reader = command.ExecuteReader()) {

						reader.Read();
						count = reader.GetInt32(0);
					}
				}
			}

			return count > 0;
		}

		public static void RemoveBook(IBook book) {
			DBManagement.DBManager.NewBookDBHandler().RemoveBook(book);
		}


		public static byte[] GetItemImage(int libraryID) {
			byte[] image = null;
			using (SqlConnection connection = DBManagement.DBManager.GetSqlConnection()) {
				connection.Open();

				using (SqlCommand command = new SqlCommand("GetItemImage", connection)) {
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@libraryID", libraryID);

					image = (byte[]) command.ExecuteScalar();
				}
			}

			return image;
		}

		public static IEnumerable<IBook> GetCheckedOutBooks() {
			return DBManagement.DBManager.NewBookDBHandler().GetCheckedOutBooks();
		}


	}
}
