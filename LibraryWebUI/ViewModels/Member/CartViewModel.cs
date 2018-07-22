using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using LibraryWebUI.Models;

namespace LibraryWebUI.ViewModels.Member
{
    public class CartViewModel : LoggedInViewModel
    {
		public IEnumerable<IBook> CartContents { get; set; }

    }
}
