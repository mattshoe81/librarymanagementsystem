using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryWebUI.Models;
using CoreLibrary.DBManagement;
using LibraryWebUI.ViewModels;
using CoreLibrary.Accounts;
using LibraryWebUI.ViewModels.Admin;
using LibraryWebUI.ViewModels.Member;
using CoreLibrary.Members;

namespace LibraryWebUI.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public IActionResult Logout() {
			Models.AccountRepository.LoggedInAccount = null;
			return View("Index", new LoginViewModel());
		}

		public IActionResult DirectLogin(LoginModel loginInfo) {
			AccountRepository accounts = new AccountRepository();
			IActionResult view;
			LoginViewModel viewModel = new LoginViewModel();
			if (accounts.VerifyAdminLogin(loginInfo.Email, loginInfo.Password)) {
				AccountRepository.LoggedInAccount = AccountManager.GetAccountByEmail(loginInfo.Email);
				view = View("AdminHome", new AdminHomeViewModel());
				Cart.EmptyCart();
			} else {
				if (accounts.VerifyMemberLogin(loginInfo.Email, loginInfo.Password)) {
					AccountRepository.LoggedInAccount = AccountManager.GetAccountByEmail(loginInfo.Email);
					view = View("MemberHome", new MemberHomeViewModel());
					Cart.EmptyCart();
				} else {
					viewModel.LoginErrorInfo = new string[] { "Invalid Login" };
					view = View("Index", viewModel);
				}				
			}

			return view;
		}

		[HttpGet]
		public IActionResult CreateAccount() {
			return View(new CreateAccountViewModel());
		}

		[HttpPost]
		public IActionResult CreateAccount(Account account, string passwordVerification) {
			IActionResult view;
			if (account.Password == passwordVerification) {
				if (!AccountManager.VerifyMemberEmail(account) && !AccountManager.VerifyAdminEmail(account)) {
					AccountManager.CreateMemberAccount(account);
					AccountRepository.LoggedInAccount = account;
					view = View("MemberHome");
				} else {
					view = View(new CreateAccountViewModel("Email already in use"));
				}
			} else {
				view = View(new CreateAccountViewModel("Passwords do not match"));
			}
		

			return view;
		}
    }
}
