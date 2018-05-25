using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Inventory;
using CoreLibrary.Members;

namespace CoreLibrary.Accounts
{
    public static class AccountManager
    {
		public static bool VerifyMemberEmail(IAccount account) {
			return DBManagement.DBManager.NewAccountDBHandler().VerifyMemberEmail(account);
		}

		public static bool VerifyAdminEmail(IAccount account) {
			return DBManagement.DBManager.NewAccountDBHandler().VerifyAdminEmail(account);
		}

		public static void CreateMemberAccount(IAccount account) {
			DBManagement.DBManager.NewAccountDBHandler().CreateMemberAccount(account);
		}

		public static void CreateAdminAccount(IAccount account) {
			DBManagement.DBManager.NewAccountDBHandler().CreateAdminAccount(account);
		}

		public static IAccount GetAccountByEmail(string email) {
			return DBManagement.DBManager.NewAccountDBHandler().GetAccountByEmail(email);
		}

		public static IEnumerable<IAccount> GetAccounts() {
			return DBManagement.DBManager.NewAccountDBHandler().GetAccounts();
		}

		public static void CheckoutMovie(IMovie movie, IAccount borrower) {
			DBManagement.DBManager.NewAccountDBHandler().CheckoutMovie(movie, borrower);
		}

		public static void CheckoutBook(IBook book, IAccount account) {
			DBManagement.DBManager.NewAccountDBHandler().CheckoutBook(book, account);
		}
	}
}
