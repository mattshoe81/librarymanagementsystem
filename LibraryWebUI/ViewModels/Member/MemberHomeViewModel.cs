using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using LibraryWebUI.Models;
using LibraryWebUI.ViewModels.Admin;

namespace LibraryWebUI.ViewModels.Member
{
    public class MemberHomeViewModel : LoggedInViewModel
    {
		public IQueryable<IBook> LoanedBooks { get; set; }
    }
}
