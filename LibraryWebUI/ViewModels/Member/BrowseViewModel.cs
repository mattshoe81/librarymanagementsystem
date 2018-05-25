using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using CoreLibrary.Searching;
using LibraryWebUI.Models;

namespace LibraryWebUI.ViewModels.Member
{
    public class BrowseViewModel
    {
		public IQueryable<IBook> Books { get; set; }
		public IQueryable<IMovie> Movies { get; set; }

		public readonly bool LoggedIn;

		public BrowseViewModel() {
			Books = SearchUtility.GetBooks().AsQueryable();
			LoggedIn = AccountRepository.LoggedInAccount != null;
		}
	}
}
