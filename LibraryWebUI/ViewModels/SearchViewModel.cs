using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using LibraryWebUI.Models;

namespace LibraryWebUI.ViewModels
{
    public class SearchViewModel
    {
		public IQueryable<IBook>  Books { get; set; }
		public IQueryable<IMovie> Movies { get; set; }

		public SearchViewModel() {
			Books = new BookRepository().Books;
		}

    }
}
