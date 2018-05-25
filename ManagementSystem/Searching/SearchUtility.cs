using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.DBManagement;
using CoreLibrary.Inventory;
using CoreLibrary.Members;

namespace CoreLibrary.Searching
{
    public class SearchUtility
    {
		public static IEnumerable<IBook> GetBooks() {
			return DBManager.NewBookDBHandler().GetBooks();
		}

		public static int BookAvailability(IBook book) {
			return DBManager.NewBookDBHandler().BookAvailability(book);
		}

		public static IEnumerable<IAccount> GetAccounts() {
			return DBManager.NewAccountDBHandler().GetAccounts();
		}

		public static IEnumerable<IBook> GetBooksByAuthor(string author) {
			return DBManager.NewBookDBHandler().GetBooksByAuthor(author);
		}

		public static IEnumerable<IBook> GetBooksByGenre(string genre) {
			return DBManager.NewBookDBHandler().GetBooksByGenre(genre);
		}

		public static IEnumerable<IBook> GetBooksByPublisher(string publisher) {
			return DBManager.NewBookDBHandler().GetBooksByPublisher(publisher);
		}

		public static IEnumerable<IBook> GetBooksByTitle(string title) {
			return DBManager.NewBookDBHandler().GetBooksByTitle(title);
		}

		public static IEnumerable<IBook> GetBooksByISBN(string ISBN) {
			return DBManager.NewBookDBHandler().GetBooksByISBN(ISBN);
		}

		public static IBook GetBookByLibraryID(int ID) {
			return DBManager.NewBookDBHandler().GetBookByLibraryID(ID);
		}

		public static IEnumerable<IMovie> GetMovies() {
			return DBManager.NewMovieDBHandler().GetMovies();
		}

		public static int MovieAvailability(IMovie movie) {
			return DBManager.NewMovieDBHandler().MovieAvailability(movie);
		}

		public static IEnumerable<IMovie> GetMoviesByDirector(string director) {
			return DBManager.NewMovieDBHandler().GetMoviesByDirector(director);
		}

		public static IEnumerable<IMovie> GetMoviesByActor(string actor) {
			return DBManager.NewMovieDBHandler().GetMoviesByActor(actor);
		}

		public static IEnumerable<IMovie> GetMoviesByMediaType(string mediaType) {
			return DBManager.NewMovieDBHandler().GetMoviesByMediaType(mediaType);
		}

		public static IEnumerable<IMovie> GetMoviesByTitle(string title) {
			return DBManager.NewMovieDBHandler().GetMoviesByTitle(title);
		}

		public static IMovie GetMovieByLibraryID(int ID) {
			return DBManager.NewMovieDBHandler().GetMovieByLibraryID(ID);
		}

		public static IAccount GetAccountByEmail(string email) {
			return DBManager.NewAccountDBHandler().GetAccountByEmail(email);
		}
	}
}
