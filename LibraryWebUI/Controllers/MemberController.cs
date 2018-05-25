using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryWebUI.Models;
using LibraryWebUI.ViewModels;
using LibraryWebUI.ViewModels.Member;
using CoreLibrary.Searching;
using CoreLibrary.Inventory;

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

			return View(loginInfo);
		}

		public IActionResult BrowseInventory() {
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

		public IActionResult ReserveBook(int libraryID) {
			ReserveBookViewModel viewModel = new ReserveBookViewModel();
			viewModel.Book = SearchUtility.GetBookByLibraryID(libraryID);

			return View(viewModel);
		}

		public IActionResult AddToCart(int libraryID) {
			IBook book = SearchUtility.GetBookByLibraryID(libraryID);
			if (!Models.Cart.IsAlreadyInCart(libraryID)) {
				Models.Cart.CartContents.Add(book);
			}
			
			return View("Cart", new CartViewModel());
		}

		public IActionResult Cart() {
			return View(new CartViewModel());
		}




	}
}