using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Members;

namespace LibraryWebUI.ViewModels
{
    public class CreateAccountViewModel : BaseViewModel
    {
		public string ErrorMessage { get; set; } = "";

		public IAccount Account { get; set; }
    }
}
