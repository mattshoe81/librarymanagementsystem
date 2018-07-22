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
using CoreLibrary.Inventory;

namespace LibraryWebUI.Controllers
{
    public class HomeController : BaseController
    {

        public IActionResult Index()
        {
            return View(this.vmFactory.GetLoginViewModel());
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
			this.LogoutUser();
			return View("Index", vmFactory.GetLoginViewModel());
		}

		[HttpPost]
		public IActionResult DirectLogin(LoginModel loginInfo) {
			AccountUtitities accounts = new AccountUtitities();
			if (accounts.VerifyAdminLogin(loginInfo.Email, loginInfo.Password)) {
				this.LoginUser(AccountManager.GetAccountByEmail(loginInfo.Email));
				return View("AdminHome", 
					new AdminHomeViewModel {
						UserAccount = this.GetUserAccount()
					}				
				);
			} else {
				if (accounts.VerifyMemberLogin(loginInfo.Email, loginInfo.Password)) {
					IAccount account = this.GetUserAccount();
					this.LoginUser(AccountManager.GetAccountByEmail(loginInfo.Email));
					account = this.GetUserAccount();
					var books = InventoryManager.GetCheckedOutBooksByUser(this.GetUserAccount().Email).AsQueryable();
					IAccount userAccount = this.GetUserAccount();
					return View("MemberHome", this.vmFactory.GetMemberHomeViewModel());
				} else {
					LoginViewModel viewModel = vmFactory.GetLoginViewModel();
					viewModel.LoginErrorInfo = new string[] { "Invalid Login" };
					return View("Index", viewModel);
				}				
			}
		}

		[HttpGet]
		public IActionResult CreateAccount() {
			return View(this.vmFactory.CreateAccountViewModel());
		}

		[HttpPost]
		public IActionResult CreateAccount(Account account, string passwordVerification) {
			IActionResult view;
			if (account.Password == passwordVerification) {
				if (!AccountManager.VerifyMemberEmail(account) && !AccountManager.VerifyAdminEmail(account)) {
					AccountManager.CreateMemberAccount(account);
					this.LoginUser(account);
					view = View(this.vmFactory.GetMemberHomeViewModel());
				} else {
					view = View(this.vmFactory.GetCreateAccountViewModel("Email already in use"));
				}
			} else {
				view = View(this.vmFactory.GetCreateAccountViewModel("Passwords do not match"));
			}
		

			return view;
		}
    }
}
