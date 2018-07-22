using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using CoreLibrary.Members;

namespace LibraryWebUI.ViewModels.Member
{
    public class ReserveBookViewModel : BaseViewModel
    {
		public IBook Book { get; set; }
	}
}
