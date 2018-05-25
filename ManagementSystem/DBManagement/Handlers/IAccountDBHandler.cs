using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Inventory;
using CoreLibrary.Members;

namespace CoreLibrary.DBManagement.Handlers
{
    public interface IAccountDBHandler
    {
		bool VerifyAdminEmail(IAccount account);

		bool VerifyMemberEmail(IAccount account);

		void CreateMemberAccount(IAccount member);

		void CreateAdminAccount(IAccount admin);

		IAccount GetAccountByEmail(string email);

		IEnumerable<IAccount> GetAccounts();

		void CheckoutMovie(IMovie movie, IAccount borrower);

		void CheckoutBook(IBook book, IAccount account);
	}
}
