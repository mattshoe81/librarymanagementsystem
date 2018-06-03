using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;

namespace LibraryWebUI.ViewModels.Admin
{
    public class LoansViewModel
    {
		public IEnumerable<IBook> Books { get; set; } = InventoryManager.GetCheckedOutBooks();
    }
}
