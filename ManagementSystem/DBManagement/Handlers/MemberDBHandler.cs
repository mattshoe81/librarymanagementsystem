using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using CoreLibrary.Members;

namespace CoreLibrary.DBManagement.Handlers
{
	class MemberDBHandler : IMemberDBHandler {

		private const string ADD_NEW_MEMBER_RESOURCE_NAME = "AddNewMember.sql";
		private const string GET_MEMBERS_RESOURCE_NAME = "GetMembers.sql";
		private const string GET_ADMINS_RESOURCE_NAME = "GetAdmins.sql";
		private const string GET_ADMIN_BY_EMAIL_RESOURCE_NAME = "GetAdminByEmail.sql";

		private MemberDBHandler() {
			// Prevent Instantiation
		}

		internal static IMemberDBHandler NewInstance() {
			return new MemberDBHandler();
		} 

		public bool AddNewAdmin(IAdmin admin) {
			throw new NotImplementedException();
		}

		public bool AddNewMember(IMember member) {
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				using (SqlCommand command = new SqlCommand(DBManager.GetQueryTextFromResource(ADD_NEW_MEMBER_RESOURCE_NAME))) {
					command.Parameters.AddWithValue("@memberID", member.MemberID);
					command.Parameters.AddWithValue("@email", member.Email);
					command.Parameters.AddWithValue("@password", member.Password);
					command.Parameters.AddWithValue("@firstName", member.FirstName);
					command.Parameters.AddWithValue("@lastName", member.LastName);
					command.ExecuteNonQuery();
				}
			}

			return true;
		}

		public IAdmin GetAdminByEmail(string email) {
			IAdmin admin = new Admin();
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				using (SqlCommand command = new SqlCommand(DBManager.GetQueryTextFromResource(GET_ADMIN_BY_EMAIL_RESOURCE_NAME))) {
					command.Connection = connection;
					command.Parameters.AddWithValue("@email", email);
					using (SqlDataReader reader = command.ExecuteReader()) {
						reader.Read();
						admin = new Admin() {
							MemberID = reader.GetInt32(reader.GetOrdinal("Member_ID")),
							Email = reader.GetString(reader.GetOrdinal("Email")),
							Password = reader.GetString(reader.GetOrdinal("Password")),
							FirstName = reader.GetString(reader.GetOrdinal("First_Name")),
							LastName = reader.GetString(reader.GetOrdinal("Last_Name"))
						};
					}
				}
			}

			return admin;
		}

		public IEnumerable<IAdmin> GetAdmins() {
			List<IAdmin> admins = new List<IAdmin>();
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				using (SqlCommand command = new SqlCommand(DBManager.GetQueryTextFromResource(GET_ADMINS_RESOURCE_NAME))) {
					command.Connection = connection;
					using (SqlDataReader reader = command.ExecuteReader()) {
						while (reader.Read()) {
							IAdmin admin = new Admin() {
								MemberID = reader.GetInt32(reader.GetOrdinal("Member_ID")),
								Email = reader.GetString(reader.GetOrdinal("Email")),
								Password = reader.GetString(reader.GetOrdinal("Password")),
								FirstName = reader.GetString(reader.GetOrdinal("First_Name")),
								LastName = reader.GetString(reader.GetOrdinal("Last_Name"))
							};
							admins.Add(admin);
						}
					}
				}
			}
			return admins;
		}

		public IEnumerable<IMember> GetMemberByEmail(string email) {
			throw new NotImplementedException();
		}

		public IEnumerable<IMember> GetMembers() {
			List<IMember> members = new List<IMember>();
			using (SqlConnection connection = DBManager.GetSqlConnection()) {
				connection.Open();
				using (SqlCommand command = new SqlCommand(DBManager.GetQueryTextFromResource(GET_MEMBERS_RESOURCE_NAME))) {
					command.Connection = connection;
					using (SqlDataReader reader = command.ExecuteReader()) {
						while (reader.Read()) {
							Member member = new Member() {
								MemberID = reader.GetInt32(reader.GetOrdinal("Member_ID")),
								Email = reader.GetString(reader.GetOrdinal("Email")),
								Password = reader.GetString(reader.GetOrdinal("Password")),
								FirstName = reader.GetString(reader.GetOrdinal("First_Name")),
								LastName = reader.GetString(reader.GetOrdinal("Last_Name"))
							};
							members.Add(member);
						}
					}
				}
			}
			return members as IEnumerable<IMember>;
		}
	}
}
