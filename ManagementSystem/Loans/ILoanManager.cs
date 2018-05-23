using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Inventory;
using CoreLibrary.Members;

namespace CoreLibrary.Loans
{
    interface ILoanManager
    {

		DateTime LoanItem(ILibraryItem item, IMember borrower);

		void ReturnItem(ILibraryItem item, Member borrower);

    }
}
