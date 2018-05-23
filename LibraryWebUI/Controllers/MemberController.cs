using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryWebUI.Models;

namespace LibraryWebUI.Controllers
{
    public class MemberController : Controller
    {
		public IActionResult MemberHome() {
			return View();
		}

		[HttpGet]
        public IActionResult MemberLogin() {
            return View();
        }

		[HttpPost]
		public IActionResult MemberLogin(LoginModel loginInfo) {
			if (this.VerifyLogin(loginInfo)) {
				return View("MemberHome");
			}

			return View(loginInfo);
		}

		public IActionResult BrowseInventory() {
			return View();
		}



















		private bool VerifyLogin(LoginModel login) {
			MemberRepository repo = new MemberRepository();
			return repo.VerifyLogin(login.Email, login.Password);
		}




    }
}