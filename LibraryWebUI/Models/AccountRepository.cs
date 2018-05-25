using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Members;
using CoreLibrary.Searching;
using LibraryWebUI.Models;

namespace LibraryWebUI.Models {
	public class AccountRepository {

		public IQueryable<IAccount> Accounts { get; private set; }

		public static IAccount LoggedInAccount { get; set; }

		public AccountRepository() {
			Accounts = SearchUtility.GetAccounts().AsQueryable();
		}

		public bool VerifyAdminLogin(string emailAddress, string password) {
			bool result = false;
			if (Accounts.Where(account => (account.Email == emailAddress) && (account.Password == password) && account.Admin).Count() > 0) {
				result = true;
			}

			return result;
		}

		public bool VerifyMemberLogin(string emailAddress, string password) {
			bool result = false;
			if (Accounts.Where(account => (account.Email == emailAddress) && (account.Password == password) && !account.Admin).Count() > 0) {
				result = true;
			}

			return result;
		}
	}
}
