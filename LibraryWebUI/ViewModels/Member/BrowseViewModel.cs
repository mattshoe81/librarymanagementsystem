using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using CoreLibrary.Members;
using CoreLibrary.Searching;
using LibraryWebUI.Models;

namespace LibraryWebUI.ViewModels.Member
{
    public class BrowseViewModel : BaseViewModel
    {
		public IQueryable<IBook> Books { get; set; } = SearchRepository.SearchResults;

		public static uint CurrentPage { get; set; } = 1;

		public static int ResultsPerPage { get; set; } = 10;

	}
}
