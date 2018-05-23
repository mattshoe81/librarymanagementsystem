using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryWebUI.Models;

namespace LibraryWebUI.Controllers
{
    public class AccountController : Controller
    {
		

		[HttpGet]
        public IActionResult RegisterUser()
        {
            return View("CreateAccount");
        }

		[HttpPost]
		public IActionResult RegisterUser(AccountModel account) {
			return View("Test", account.ToArray());

		}
    }
}