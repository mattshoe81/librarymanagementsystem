using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Reflection;
using CoreLibrary.DBManagement.Handlers;

namespace CoreLibrary.DBManagement
{
	internal class DBManager {
		private readonly static string CONNECTION_STRING;

		static DBManager(){
			//SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			//builder.DataSource = "mattshoe.database.windows.net";
			//builder.UserID = "mattshoe81";
			//builder.Password = "8122Password";
			//builder.InitialCatalog = "Library";
			//CONNECTION_STRING = builder.ConnectionString;
			CONNECTION_STRING = @"Data Source=MATTSHOESURFACE\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True";
		}

		internal static SqlConnection GetSqlConnection() {
			return new SqlConnection(DBManager.CONNECTION_STRING);
		}

		internal static string GetQueryTextFromResource(string resourceFileName) {
			string queryText = "";
			string resource = $"CoreLibrary.DBManagement.Queries.{resourceFileName}";
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
			using (StreamReader reader = new StreamReader(stream)) {
				queryText = reader.ReadToEnd();
			}
			return queryText;
		}

		internal static IBookDBHandler NewBookDBHandler() {
			return BookDBHandler.NewInstance();
		}

		internal static IMovieDBHandler NewMovieDBHandler() {
			return MovieDBHandler.NewInstance();
		}

		public static IAccountDBHandler NewAccountDBHandler() {
			return AccountDBHandler.NewInstance();
		}
	}
}
