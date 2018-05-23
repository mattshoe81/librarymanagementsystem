using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using CoreLibrary.Searching;
using CoreLibrary.Inventory;
using CoreLibrary.Members;
using LibraryWebUI.ViewModels.Admin;
using LibraryWebUI.ViewModels;

namespace LibraryWebUI.Controllers
{
    public class AdminController : Controller
    {
		[HttpGet]
        public IActionResult AdminLogin()
        {
            return View(new LoginViewModel());
        }

		[HttpPost]
		public IActionResult AdminLogin(LoginModel loginInfo) {
			IActionResult view;
			AdminRepository adminRepo = new AdminRepository();
			if (ModelState.IsValid) {
				if (adminRepo.VerifyLogin(loginInfo.Email, loginInfo.Password)) {
					IAdmin admin = SearchUtility.GetAdminByEmail(loginInfo.Email);
					AdminRepository.LoggedInAdmin = admin;
					view = View("AdminHome", new AdminHomeViewModel());
				} else {
					view = View(new LoginViewModel {
						LoginErrorInfo = new string[] { "Incorrect login information. Please try again" },
						LoginInfo = new LoginModel { Email = loginInfo.Email }
					});
				}
			} else {
				view = View(new LoginViewModel());
			}

			return view;
		}

		public IActionResult AdminHome() {
			IActionResult view;
			if (AdminRepository.LoggedInAdmin == null) {
				view = View("AdminLogin", new LoginViewModel());
			} else {
				view = View(new AdminHomeViewModel());
			}
			return view;
		}

		public IActionResult Inventory() {

			return View();
		}

		[HttpGet]
		public IActionResult NewBook() {
			return View(new NewBookViewModel());
		}

		[HttpPost]
		public IActionResult NewBook(Book book) {
			book.LibraryID = InventoryManager.GetNewLibraryID();
			CoreLibrary.Inventory.InventoryManager.AddNewBook(book);
			NewBookViewModel viewModel = new NewBookViewModel();
			viewModel.Book = book;

			return View("NewBookResult", viewModel);
		}
		
		public IActionResult EditBook(int libraryID) {
			EditBookViewModel viewModel = new EditBookViewModel();
			viewModel.Book = SearchUtility.GetBookByLibraryID(libraryID);
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult EditBookResult(Book book) {
			InventoryManager.UpdateBook(book);
			EditBookResultViewModel viewModel = new EditBookResultViewModel();
			viewModel.Book = SearchUtility.GetBookByLibraryID(book.LibraryID);
			return View(viewModel);
		}

		[HttpGet]
		public IActionResult SpecifyBookToEdit() {
			return View(new SpecifyBookViewModel());
		}

		[HttpPost]
		public IActionResult SpecifyBookToEdit(int libraryID) {
			if (InventoryManager.IsValidLibraryID(libraryID)) {
				EditBookViewModel viewModel = new EditBookViewModel();
				viewModel.Book = SearchUtility.GetBookByLibraryID(libraryID);
				return View("EditBook", viewModel);
			}
			
			return View(new SpecifyBookViewModel("Invalid Library ID"));
		}

		public IActionResult SpecifyBookToRemove(int libraryID) {
			return View(new SpecifyBookViewModel());
		}

		public IActionResult RemoveBook(int libraryID) {
			RemoveBookViewModel viewModel = new RemoveBookViewModel();
			if (InventoryManager.IsValidLibraryID(libraryID)) {
				viewModel.Book = SearchUtility.GetBookByLibraryID(libraryID);
			} else {
				return View("SpecifyBookToRemove", new SpecifyBookViewModel("Invalid Library ID"));
			}
			
			return View(viewModel);
		}

		public IActionResult RemoveBookResult(int libraryID) {
			InventoryManager.RemoveBook(SearchUtility.GetBookByLibraryID(libraryID));
			return View();
		}

		public IActionResult Search() {
			return View(new SearchViewModel());
		}

		public IActionResult BookDetails(int libraryID) {
			IBook book = SearchUtility.GetBookByLibraryID(libraryID);
			BookDetailsViewModel viewModel = new BookDetailsViewModel();
			viewModel.Book = book;
			return View(viewModel);
		}
    }
}