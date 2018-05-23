using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;

namespace LibraryWebUI.ViewModels.Admin
{
    public class RemoveBookViewModel
    {
		public IBook Book { get; set; }
		public string ErrorMessage { get; set; }
    }
}
