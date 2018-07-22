using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Accounts;
using CoreLibrary.Inventory;
using CoreLibrary.Members;
using LibraryWebUI.Models;
using LibraryWebUI.ViewModels.Admin;

namespace LibraryWebUI.ViewModels.Member
{
    public class MemberHomeViewModel : BaseViewModel
    {
		public IQueryable<IBook> LoanedBooks { get; set; } = (new List<IBook>()).AsQueryable();
	}
}
