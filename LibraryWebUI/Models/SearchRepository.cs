using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using CoreLibrary.Searching;

namespace LibraryWebUI.Models
{
    public class SearchRepository
    {
		public static IQueryable<IBook> SearchResults { get; set; } = SearchUtility.GetBooks().AsQueryable().OrderBy(book => book.Title);

		public static int ResultsCount => SearchResults.Count();

		public static void Reset() {
			SearchResults = SearchUtility.GetBooks().AsQueryable().OrderBy(book => book.Title);
		}
	}
}
