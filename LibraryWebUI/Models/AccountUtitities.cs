using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Members;
using CoreLibrary.Searching;
using LibraryWebUI.Models;

namespace LibraryWebUI.Models {
	public class AccountUtitities {


		public bool VerifyAdminLogin(string emailAddress, string password) {
			IQueryable<IAccount> accounts = SearchUtility.GetAccounts().AsQueryable();
			bool result = false;
			if (accounts.Where(account => (account.Email == emailAddress) && (account.Password == password) && account.Admin).Count() > 0) {
				result = true;
			}

			return result;
		}

		public bool VerifyMemberLogin(string emailAddress, string password) {
			IQueryable<IAccount> accounts = SearchUtility.GetAccounts().AsQueryable();
			bool result = false;
			if (accounts.Where(account => (account.Email == emailAddress) && (account.Password == password) && !account.Admin).Count() > 0) {
				result = true;
			}

			return result;
		}
	}
}
