using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using CoreLibrary.Members;
using LibraryWebUI.Models;

namespace LibraryWebUI.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
		public IQueryable<IBook>  Books { get; set; }
		public IQueryable<IMovie> Movies { get; set; }

	}
}
