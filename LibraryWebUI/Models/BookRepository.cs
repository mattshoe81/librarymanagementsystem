using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using CoreLibrary.Searching;

namespace LibraryWebUI.Models
{
    public class BookRepository
    {
		public IQueryable<IBook> Books { get; set; }

		public BookRepository() {
			Books = SearchUtility.GetBooks().AsQueryable();
		}
    }
}
