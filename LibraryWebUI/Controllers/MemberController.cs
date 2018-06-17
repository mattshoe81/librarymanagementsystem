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
using System.Net.Mail;
using CoreLibrary.Accounts;

namespace LibraryWebUI.Controllers
{
    public class MemberController : Controller
    {
		public delegate void HandleSentEmail();

		public IActionResult MemberHome() {
			return View(new MemberHomeViewModel());
		}

		[HttpGet]
        public IActionResult MemberLogin() {
            return View();
        }

		[HttpPost]
		public IActionResult MemberLogin(LoginModel loginInfo) {

			return View(loginInfo);
		}

		[HttpPost]
		public IActionResult BrowseInventory() {
			SearchRepository.SearchResults = SearchUtility.GetBooks().AsQueryable().OrderBy(book => book.Title);
			BrowseViewModel.CurrentPage = 1;
			return View(new BrowseViewModel());
		}

		public IActionResult NewBrowseSession() {
			SearchRepository.SearchResults = SearchUtility.GetBooks().AsQueryable().OrderBy(book => book.Title);
			BrowseViewModel.CurrentPage = 1;
			return RedirectToAction("BrowseInventory", new { page = BrowseViewModel.CurrentPage });
		}

		[HttpGet]
		public IActionResult BrowseInventory(int page) {
			return View(new BrowseViewModel());
		}

		public IActionResult NextPage() {
			BrowseViewModel.CurrentPage++;
			return RedirectToAction("BrowseInventory", new { page=BrowseViewModel.CurrentPage });
			//return View("BrowseInventory", new BrowseViewModel());
		}

		public IActionResult PreviousPage() {
			BrowseViewModel.CurrentPage--;
			return RedirectToAction("BrowseInventory", new { page = BrowseViewModel.CurrentPage });
		}

		public IActionResult SetResultsPerPage(int resultsPerPage) {
			BrowseViewModel.ResultsPerPage = resultsPerPage;
			BrowseViewModel.CurrentPage = 1;
			return RedirectToAction("BrowseInventory");
		}

		[HttpPost]
		public IActionResult SortSearch(string sortSearchBy) {			
			switch (sortSearchBy) {
				case "title":
					SearchRepository.SearchResults = SearchRepository.SearchResults.OrderBy(book => book.Title);
					break;
				case "author":
					SearchRepository.SearchResults = SearchRepository.SearchResults.OrderBy(book => book.Author);
					break;
				case "libraryID":
					SearchRepository.SearchResults = SearchRepository.SearchResults.OrderBy(book => book.LibraryID);
					break;
				case "genre":
					SearchRepository.SearchResults = SearchRepository.SearchResults.OrderBy(book => book.Genre);
					break;
				case "format":
					SearchRepository.SearchResults = SearchRepository.SearchResults.OrderBy(book => book.Format);
					break;
				default:
					break;
			}
			BrowseViewModel.CurrentPage = 1;

			return RedirectToAction("BrowseInventory");
			;
		}

		[HttpPost]
		public IActionResult SearchByUserString(string searchString) {
			SearchRepository.Reset();
			SearchRepository.SearchResults = SearchRepository.SearchResults.Where(book => book.Title.ToLower().Contains(searchString.ToLower())
															|| book.Author.ToLower().Contains(searchString.ToLower())
															|| book.Genre.ToLower().Contains(searchString.ToLower())
															|| book.ISBN10.ToLower().Contains(searchString.ToLower())
															|| book.ISBN13.ToLower().Contains(searchString.ToLower())
															|| book.Description.ToLower().Contains(searchString.ToLower())
															|| book.Format.ToLower().Contains(searchString.ToLower())
															|| book.Publisher.ToLower().Contains(searchString.ToLower())
														);
			BrowseViewModel.CurrentPage = 1;

			return RedirectToAction("BrowseInventory");
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
			
			return View("BrowseInventory", new BrowseViewModel());
		}

		public IActionResult Cart() {
			return View(new CartViewModel());
		}

		public IActionResult BookDetails(int libraryID) {
			IBook book = SearchUtility.GetBookByLibraryID(libraryID);
			BookDetailsViewModel viewModel = new BookDetailsViewModel();
			viewModel.Book = book;
			return View(viewModel);
		}

		public IActionResult Checkout() {
            this.EmailAdministrator();
            this.EmailMember();



			foreach (IBook book in Models.Cart.CartContents) {
				AccountManager.CheckoutBook(book, Models.AccountRepository.LoggedInAccount);
			}

			Models.Cart.EmptyCart();
			return View("MemberHome", new MemberHomeViewModel());
		}
		
		[HttpPost]
		public IActionResult RemoveFromCart(int libraryID) {
			Models.Cart.CartContents.Remove(SearchUtility.GetBookByLibraryID(libraryID));
			return View("Cart", new CartViewModel());
		}

		public IActionResult AccountDetails() {
			return View();
		}

        private void EmailAdministrator()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("mattslibrarymanager@gmail.com");
                mail.To.Add("mattslibrarymanager@gmail.com");
                mail.Subject = "Book Reservation!";

                string bodyMessage = $"Member: {AccountRepository.LoggedInAccount.FirstName} {AccountRepository.LoggedInAccount.LastName} \n" +
                    $"Email: {AccountRepository.LoggedInAccount.Email}\n\n" +
                    $"Reserve these items:\n====================\n";
                foreach (IBook item in Models.Cart.CartContents)
                {
                    bodyMessage += $"Item# {item.LibraryID}\n{item.Title}\n====================\n";
                }
                mail.Body = bodyMessage;

                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential("mattslibrarymanager@gmail.com", "8122Password");
                smtpServer.EnableSsl = true;

                HandleSentEmail sentEmailHandler = () => {
                    smtpServer.Dispose();
                    mail.Dispose();
                    RedirectToAction("BrowseInventory");
                };
                smtpServer.SendAsync(mail, sentEmailHandler);
            }
            catch (Exception e)
            {
                
            }
        }

        private void EmailMember()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("mattslibrarymanager@gmail.com");
                mail.To.Add(AccountRepository.LoggedInAccount.Email);
                mail.Subject = "Reservation Summary!";
                
                string bodyMessage = "Thank you for your order! \nYou may reply to this email at any time to set up a time for pick up or delivery.\n\nDetails:\n\n====================\n";
                foreach (IBook item in Models.Cart.CartContents)
                {
                    bodyMessage += $"Item# {item.LibraryID}\n{item.Title}\n====================\n";
                }

                bodyMessage += "\n\nYou may reply to this email at any time if you have questions or to cancel your order.";
                mail.Body = bodyMessage;

                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential("mattslibrarymanager", "8122Password");
                smtpServer.EnableSsl = true;

                HandleSentEmail sentEmailHandler = () => {
                    smtpServer.Dispose();
                    mail.Dispose();
                    RedirectToAction("BrowseInventory");
                };
                smtpServer.SendAsync(mail, sentEmailHandler);
            }
            catch (Exception e)
            {
                
            }
        }



	}
}