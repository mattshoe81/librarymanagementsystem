using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using CoreLibrary.Members;

namespace LibraryWebUI.ViewModels
{
    public class BookDetailsViewModel : BaseViewModel
    {

		public IBook Book { get; set; }

		public string Action { get; set; }

	}
}
