using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Members;
using LibraryWebUI.Controllers;
using LibraryWebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebUI.ViewModels
{
	public abstract class BaseViewModel {

		public IAccount UserAccount { get; set; }

		public Cart UserCart { get; set; }
    }
}
