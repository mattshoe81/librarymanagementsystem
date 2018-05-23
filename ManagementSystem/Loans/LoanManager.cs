using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Inventory;
using CoreLibrary.Members;
using CoreLibrary.DBManagement;

namespace CoreLibrary.Loans
{
	public class LoanManager : ILoanManager {

		public DateTime LoanItem(ILibraryItem item, IMember borrower) {
			DateTime dueDate;
			IDBHandler dbHandler = DBManager.NewDBHandler();
			switch (item.Format) {
				case MediaFormat.PAPERBACK:
				case MediaFormat.HARDCOVER:
					IBook book = dbHandler.GetBookByLibraryID(item.LibraryID);
					dbHandler.CheckoutBook(book, borrower);
					dueDate = DateTime.Today.AddDays(book.LengthOfLoan);
					break;
				case MediaFormat.DVD:
				case MediaFormat.BLURAY:
					IMovie movie = dbHandler.GetMovieByLibraryID(item.LibraryID);
					dbHandler.CheckoutMovie(movie, borrower);
					dueDate = DateTime.Today.AddDays(movie.LengthOfLoan);
					break;
				default:
					dueDate = DateTime.Today;
					break;
			}

			return dueDate;
		}

		public void ReturnItem(ILibraryItem item, Member borrower) {
			IDBHandler dBHandler = DBManager.NewDBHandler();
			switch (item.Format) {
				case MediaFormat.PAPERBACK:
				case MediaFormat.HARDCOVER:
					IBook book = dBHandler.GetBookByLibraryID(item.LibraryID);
					dBHandler.ReturnBook(book);
					break;
				case MediaFormat.DVD:
				case MediaFormat.BLURAY:
					IMovie movie = dBHandler.GetMovieByLibraryID(item.LibraryID);
					dBHandler.ReturnMovie(movie, borrower);
					break;
				default:
					break;
			}
		}
	}
}
