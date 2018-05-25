using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Inventory;
using CoreLibrary.Members;
using CoreLibrary.DBManagement;
using CoreLibrary.DBManagement.Handlers;

namespace CoreLibrary.Loans
{
	public class LoanManager : ILoanManager {

		public DateTime LoanItem(ILibraryItem item, IAccount borrower) {
			DateTime dueDate;
			IAccountDBHandler dbHandler = DBManager.NewAccountDBHandler();
			switch (item.Format) {
				case MediaFormat.PAPERBACK:
				case MediaFormat.HARDCOVER:
					IBook book = Searching.SearchUtility.GetBookByLibraryID(item.LibraryID);
					dbHandler.CheckoutBook(book, borrower);
					dueDate = DateTime.Today.AddDays(book.LengthOfLoan);
					break;
				case MediaFormat.DVD:
				case MediaFormat.BLURAY:
					IMovie movie = Searching.SearchUtility.GetMovieByLibraryID(item.LibraryID);
					dbHandler.CheckoutMovie(movie, borrower);
					dueDate = DateTime.Today.AddDays(movie.LengthOfLoan);
					break;
				default:
					dueDate = DateTime.Today;
					break;
			}

			return dueDate;
		}

		public void ReturnItem(ILibraryItem item, IAccount borrower) {
			throw new NotImplementedException();
		}
	}
}
