using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using CoreLibrary.Members;
using LibraryWebUI.Models;

namespace LibraryWebUI.ViewModels.Member
{
    public class CartViewModel : BaseViewModel
    {
		public IEnumerable<IBook> CartContents { get; set; }

    }
}
