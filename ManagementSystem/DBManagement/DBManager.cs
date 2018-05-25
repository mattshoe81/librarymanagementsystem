using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Reflection;
using CoreLibrary.DBManagement.Handlers;

namespace CoreLibrary.DBManagement
{
    internal static class DBManager
    {
		private const string CONNECTION_STRING = @"Data Source=MATTSHOESURFACE\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True";

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
