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
        public IActionResult AdminLogin() {
            return View(new LoginViewModel());
		}

		[HttpPost]
		public IActionResult AdminLogin(LoginModel loginInfo) {
			IActionResult view;
			AccountRepository adminRepo = new AccountRepository();
			if (ModelState.IsValid) {
				if (adminRepo.VerifyAdminLogin(loginInfo.Email, loginInfo.Password)) {
					IAccount admin = SearchUtility.GetAccountByEmail(loginInfo.Email);
					AccountRepository.LoggedInAccount = admin;
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
			if (AccountRepository.LoggedInAccount == null) {
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
			SpecifyBookViewModel viewModel = new SpecifyBookViewModel();
			viewModel.Action = "SpecifyBookToEdit";
			return View("SpecifyBook", viewModel);
		}

		[HttpPost]
		public IActionResult SpecifyBookToEdit(int libraryID) {
			if (InventoryManager.IsValidLibraryID(libraryID)) {
				EditBookViewModel viewModel = new EditBookViewModel();
				viewModel.Book = SearchUtility.GetBookByLibraryID(libraryID);
				return View("EditBook", viewModel);
			}

			return View(new SpecifyBookViewModel {
				ErrorMessage = "Invalid Library ID",
				Action = "SpecifyBookToEdit"
			});
		}

		public IActionResult SpecifyBookToRemove(int libraryID) {
			SpecifyBookViewModel viewModel = new SpecifyBookViewModel();
			viewModel.Action = "SpecifyBookToRemove";
			return View(viewModel);
		}

		public IActionResult RemoveBook(int libraryID) {
			RemoveBookViewModel viewModel = new RemoveBookViewModel();
			if (InventoryManager.IsValidLibraryID(libraryID)) {
				viewModel.Book = SearchUtility.GetBookByLibraryID(libraryID);
			} else {
				return View("SpecifyBookToRemove", new SpecifyBookViewModel {
					ErrorMessage = "Invalid Library ID",
					Action = "SpecifyBookToRemove"
				});
			}
			
			return View(viewModel);
		}

		public IActionResult RemoveBookResult(int libraryID) {
			InventoryManager.RemoveBook(SearchUtility.GetBookByLibraryID(libraryID));
			return View();
		}

		[HttpGet]
		public IActionResult Search() {
			SearchViewModel viewModel = new SearchViewModel();
			return View(new SearchViewModel());
		}

		[HttpPost]
		public IActionResult SortSearch(string sortSearchBy) {
			SearchViewModel viewModel = new SearchViewModel();
			switch (sortSearchBy) {
				case "title":
					viewModel.Books = viewModel.Books.OrderBy(book => book.Title);
					break;
				case "author":
					viewModel.Books = viewModel.Books.OrderBy(book => book.Author);
					break;
				case "libraryID":
					viewModel.Books = viewModel.Books.OrderBy(book => book.LibraryID);
					break;
				default:
					break;
			}

			return View("BrowseInventory", viewModel);
		}

		[HttpPost]
		public IActionResult SearchByUserString(string searchString) {
			SearchViewModel viewModel = new SearchViewModel();
			viewModel.Books = viewModel.Books.Where(book => book.Title.ToLower().Contains(searchString.ToLower())
															|| book.Author.ToLower().Contains(searchString.ToLower())
															|| book.Genre.ToLower().Contains(searchString.ToLower())
															|| book.ISBN10.ToLower().Contains(searchString.ToLower())
															|| book.ISBN13.ToLower().Contains(searchString.ToLower())
															|| book.Description.ToLower().Contains(searchString.ToLower())
															|| book.Format.ToLower().Contains(searchString.ToLower())
															|| book.Publisher.ToLower().Contains(searchString.ToLower())
														);
			return View("BrowseInventory", viewModel);
		}

		public IActionResult BookDetails(int libraryID) {
			IBook book = SearchUtility.GetBookByLibraryID(libraryID);
			BookDetailsViewModel viewModel = new BookDetailsViewModel();
			viewModel.Book = book;
			return View(viewModel);
		}


    }
}