using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Members;
using LibraryWebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebUI.ViewModels
{
	public abstract class LoggedInViewModel {

		public IAccount UserAccount { get; set; }
    }
}
