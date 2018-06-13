using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using CoreLibrary.Inventory;
using CoreLibrary.Members;

namespace CoreLibrary.DBManagement.Handlers
{
	class AccountDBHandler : IAccountDBHandler {

		private const string CREATE_MEMBER_ACCOUNT_RESOURCE = "AccountQueries.CreateMemberAccount.sql";
		private const string CREATE_ADMIN_ACCOUNT_RESOURCE = "AccountQueries.CreateAdminAccount.sql";
		private const string GET_ACCOUNT_BY_EMAIL_RESOURCE = "AccountQueries.GetAccountByEmail.sql";
		private const string GET_ACCOUNTS_RESOURCE = "AccountQueries.GetAccounts.sql";
		private const string VERIFY_ADMIN_EMAIL_RESOURCE = "AccountQueries.VerifyAdminEmail.sql";
		private const string VERIFY_MEMBER_EMAIL_RESOURCE = "AccountQueries.VerifyMemberEmail.sql";


		private AccountDBHandler() {
			// Prevent Instantiation
		}

		internal static IAccountDBHandler NewInstance() {
			return new AccountDBHandler();
		} 

		public void CreateMemberAccount(IAccount account) {
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				using (SqlCommand command = new SqlCommand(StoredProcedures.CREATE_MEMBER_ACCOUNT, connection)) {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@email", account.Email);
					command.Parameters.AddWithValue("@password", account.Password);
					command.Parameters.AddWithValue("@firstName", account.FirstName);
					command.Parameters.AddWithValue("@lastName", account.LastName);
					command.ExecuteNonQuery();
				}
			}
		}

		public void CreateAdminAccount(IAccount account) {
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				using (SqlCommand command = new SqlCommand(DBManager.GetQueryTextFromResource(CREATE_ADMIN_ACCOUNT_RESOURCE), connection)) {
					command.Parameters.AddWithValue("@email", account.Email);
					command.Parameters.AddWithValue("@password", account.Password);
					command.Parameters.AddWithValue("@firstName", account.FirstName);
					command.Parameters.AddWithValue("@lastName", account.LastName);
					command.ExecuteNonQuery();
				}
			}
		}

		public IAccount GetAccountByEmail(string email) {
			IAccount admin = new Account();
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				using (SqlCommand command = new SqlCommand(DBManager.GetQueryTextFromResource(GET_ACCOUNT_BY_EMAIL_RESOURCE))) {
					command.Connection = connection;
					command.Parameters.AddWithValue("@email", email);
					using (SqlDataReader reader = command.ExecuteReader()) {
						reader.Read();
						admin = new Account() {
							Email = reader.GetString(reader.GetOrdinal("Email")),
							Password = reader.GetString(reader.GetOrdinal("Password")),
							FirstName = reader.GetString(reader.GetOrdinal("First_Name")),
							LastName = reader.GetString(reader.GetOrdinal("Last_Name")),
							Admin = reader.GetBoolean(reader.GetOrdinal("Admin"))
						};
					}
				}
			}

			return admin;
		}

		public bool VerifyAdminEmail(IAccount account) {
			bool isAdmin = false;
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				using (SqlCommand command = new SqlCommand(DBManager.GetQueryTextFromResource(VERIFY_ADMIN_EMAIL_RESOURCE))) {
					command.Connection = connection;
					command.Parameters.AddWithValue("@email", account.Email);
					using (SqlDataReader reader = command.ExecuteReader()) {
						reader.Read();
						isAdmin = reader.GetBoolean(0);
					}
				}
			}

			return isAdmin;
		}

		public bool VerifyMemberEmail(IAccount account) {
			bool isMember = false;
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				using (SqlCommand command = new SqlCommand(DBManager.GetQueryTextFromResource(VERIFY_MEMBER_EMAIL_RESOURCE))) {
					command.Connection = connection;
					command.Parameters.AddWithValue("@email", account.Email);
					using (SqlDataReader reader = command.ExecuteReader()) {
						reader.Read();
						isMember = reader.GetBoolean(0);
					}
				}
			}

			return isMember;
		}

		public void CheckoutBook(IBook book, IAccount account) {
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				using (SqlCommand command = new SqlCommand(StoredProcedures.CHECKOUT_BOOK, connection)) {
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@libraryID", book.LibraryID);
					command.Parameters.AddWithValue("@title", book.Title);
					command.Parameters.AddWithValue("@checkoutDate", DateTime.Today);
					command.Parameters.AddWithValue("@duedate", DateTime.Today.AddDays(book.LengthOfLoan));
					command.Parameters.AddWithValue("@mediaType", MediaFormat.GetMediaKey(book.Format));
					command.Parameters.AddWithValue("@borrowerEmail", account.Email);
					command.ExecuteNonQuery();
				}
			}
		}

		public void CheckoutMovie(IMovie movie, IAccount borrower) {
			throw new NotImplementedException();
		}

		public IEnumerable<IAccount> GetAccounts() {
			List<IAccount> accounts = new List<IAccount>();
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				using (SqlCommand command = new SqlCommand(DBManager.GetQueryTextFromResource(GET_ACCOUNTS_RESOURCE))) {
					command.Connection = connection;
					using (SqlDataReader reader = command.ExecuteReader()) {
						while (reader.Read()) {
							IAccount account = new Account() {
								Email = reader.GetString(reader.GetOrdinal("Email")),
								Password = reader.GetString(reader.GetOrdinal("Password")),
								FirstName = reader.GetString(reader.GetOrdinal("First_Name")),
								LastName = reader.GetString(reader.GetOrdinal("Last_Name")),
								Admin = reader.GetBoolean(reader.GetOrdinal("Admin"))
							};
							accounts.Add(account);
						}
					}
				}
			}

			return accounts;
		}
	}
}
