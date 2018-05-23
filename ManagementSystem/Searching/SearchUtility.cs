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
			return DBManager.NewDBHandler().GetBooks();
		}

		public static int BookAvailability(IBook book) {
			return DBManager.NewDBHandler().BookAvailability(book);
		}

		public static IEnumerable<IBook> GetBooksByAuthor(string author) {
			return DBManager.NewDBHandler().GetBooksByAuthor(author);
		}

		public static IEnumerable<IBook> GetBooksByGenre(string genre) {
			return DBManager.NewDBHandler().GetBooksByGenre(genre);
		}

		public static IEnumerable<IBook> GetBooksByPublisher(string publisher) {
			return DBManager.NewDBHandler().GetBooksByPublisher(publisher);
		}

		public static IEnumerable<IBook> GetBooksByTitle(string title) {
			return DBManager.NewDBHandler().GetBooksByTitle(title);
		}

		public static IEnumerable<IBook> GetBooksByISBN(string ISBN) {
			return DBManager.NewDBHandler().GetBooksByISBN(ISBN);
		}

		public static IBook GetBookByLibraryID(int ID) {
			return DBManager.NewDBHandler().GetBookByLibraryID(ID);
		}

		public static IEnumerable<IMovie> GetMovies() {
			return DBManager.NewDBHandler().GetMovies();
		}

		public static int MovieAvailability(IMovie movie) {
			return DBManager.NewDBHandler().MovieAvailability(movie);
		}

		public static IEnumerable<IMovie> GetMoviesByDirector(string director) {
			return DBManager.NewDBHandler().GetMoviesByDirector(director);
		}

		public static IEnumerable<IMovie> GetMoviesByActor(string actor) {
			return DBManager.NewDBHandler().GetMoviesByActor(actor);
		}

		public static IEnumerable<IMovie> GetMoviesByMediaType(string mediaType) {
			return DBManager.NewDBHandler().GetMoviesByMediaType(mediaType);
		}

		public static IEnumerable<IMovie> GetMoviesByTitle(string title) {
			return DBManager.NewDBHandler().GetMoviesByTitle(title);
		}

		public static IMovie GetMovieByLibraryID(int ID) {
			return DBManager.NewDBHandler().GetMovieByLibraryID(ID);
		}

		public static IEnumerable<IAdmin> GetAdmins() {
			return DBManager.NewMemberDBHandler().GetAdmins();
		}

		public static IAdmin GetAdminByEmail(string email) {
			return DBManager.NewMemberDBHandler().GetAdminByEmail(email);
		}
	}
}
