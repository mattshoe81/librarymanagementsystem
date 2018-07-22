using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;

namespace LibraryWebUI.ViewModels.Member
{
    public class ReserveBookViewModel : LoggedInViewModel
    {
		public IBook Book { get; set; }
    }
}
