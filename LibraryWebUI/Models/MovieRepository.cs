using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using CoreLibrary.Searching;

namespace LibraryWebUI.Models
{
    public class MovieRepository
    {
		public IQueryable<IMovie> Movies { get; set; }

		public MovieRepository() {
			Movies = SearchUtility.GetMovies().AsQueryable();
		}
	}
}
