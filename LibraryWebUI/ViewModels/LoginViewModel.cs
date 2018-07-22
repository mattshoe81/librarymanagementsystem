using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Members;

namespace LibraryWebUI.ViewModels
{
	public class LoginViewModel : BaseViewModel {

		public Models.LoginModel LoginInfo { get; set; } = new Models.LoginModel { Email = "", Password = "" };

		public string[] LoginErrorInfo { get; set; } = { "" };
	}
}
