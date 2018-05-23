using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Members;
using CoreLibrary.Searching;
using LibraryWebUI.Models;

namespace LibraryWebUI.Models {
	public class AdminRepository {

		public IQueryable<IAdmin> Admins { get; set; }

		public static IAdmin LoggedInAdmin { get; set; }

		public AdminRepository() {
			Admins = SearchUtility.GetAdmins().AsQueryable();
		}

		public bool VerifyLogin(string emailAddress, string password) {
			bool result = false;
			if (Admins.Where(admin => admin.Email == emailAddress).Count() > 0) {
				result = true;
				if (Admins.Where(admin => admin.Password == password).Count() <= 0) {
					result = false;
				}
			}

			return result;
		}
	}
}
