using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;

namespace LibraryWebUI.ViewModels
{
    public class BookDetailsViewModel : LoggedInViewModel
    {
		public IBook Book { get; set; }

		public string Action { get; set; }
    }
}
