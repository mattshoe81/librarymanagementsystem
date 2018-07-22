using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using CoreLibrary.Members;

namespace LibraryWebUI.ViewModels.Admin
{
	public class NewBookViewModel{

		public IAccount LoggedInAdmin { get; set; }
		
		public IBook Book { get; set; }

		public void ClearBook() {
			Book = new Book {
				Title = "",
				Author = "",
				ISBN10 = "",
				ISBN13 = "",
				Length = 0,
				Genre = "",
				Publisher = "",
				PublicationYear = DateTime.Today.Year,
				Description = ""
			};
		}

		public NewBookViewModel() {
			Book = new Book {
				Title = "",
				Author = "",
				ISBN10 = "",
				ISBN13 = "",
				Length = 0,
				Genre = "",
				Publisher = "",
				PublicationYear = DateTime.Today.Year,
				Description = ""
			};
		}

	}
}
