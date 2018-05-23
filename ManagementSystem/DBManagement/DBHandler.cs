using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Diagnostics;
using CoreLibrary.Inventory;
using System.Threading.Tasks;
using CoreLibrary.Members;
using CoreLibrary.DBManagement.Handlers;

namespace CoreLibrary.DBManagement
{
    internal class DBHandler : IDBHandler {

		private DBHandler() {
			// Prevent direct instantiation
		}

		internal static IDBHandler NewInstance() {
			return new DBHandler();
		}

		public bool AddNewAdmin(IAdmin admin) {
			return MemberDBHandler.NewInstance().AddNewAdmin(admin);
		}

		public bool AddNewBook(IBook book) {
			return BookDBHandler.NewInstance().AddNewBook(book);
		}

		public bool AddNewMember(IMember member) {
			return MemberDBHandler.NewInstance().AddNewMember(member);
		}

		public bool AddNewMovie(IMovie movie) {
			return MovieDBHandler.NewInstance().AddNewMovie(movie);
		}

		public int BookAvailability(IBook book) {
			return BookDBHandler.NewInstance().BookAvailability(book);
		}

		public bool CheckoutBook(IBook book, IMember member) {
			return BookDBHandler.NewInstance().CheckoutBook(book, member);
		}

		public bool CheckoutMovie(IMovie movie, IMember member) {
			return MovieDBHandler.NewInstance().CheckoutMovie(movie, member);
		}

		public IAdmin GetAdminByEmail(string email) {
			return MemberDBHandler.NewInstance().GetAdminByEmail(email);
		}

		public IEnumerable<IAdmin> GetAdmins() {
			return MemberDBHandler.NewInstance().GetAdmins();
		}

		public IBook GetBookByLibraryID(int ID) {
			return BookDBHandler.NewInstance().GetBookByLibraryID(ID);
		}

		public IEnumerable<IBook> GetBooks() {
			return BookDBHandler.NewInstance().GetBooks();
		}

		public IEnumerable<IBook> GetBooksByAuthor(string author) {
			return BookDBHandler.NewInstance().GetBooksByAuthor(author);
		}

		public IEnumerable<IBook> GetBooksByGenre(string genre) {
			return BookDBHandler.NewInstance().GetBooksByGenre(genre);
		}

		public IEnumerable<IBook> GetBooksByISBN(string ISBN) {
			return BookDBHandler.NewInstance().GetBooksByISBN(ISBN);
		}

		public IEnumerable<IBook> GetBooksByPublisher(string publisher) {
			return BookDBHandler.NewInstance().GetBooksByPublisher(publisher);
		}

		public IEnumerable<IBook> GetBooksByTitle(string title) {
			return BookDBHandler.NewInstance().GetBooksByTitle(title);
		}

		public IEnumerable<IBook> GetCheckedOutBooks() {
			return BookDBHandler.NewInstance().GetCheckedOutBooks();
		}

		public IEnumerable<IMovie> GetCheckedOutMovies() {
			return MovieDBHandler.NewInstance().GetCheckedOutMovies();
		}

		public IEnumerable<IMember> GetMemberByEmail(string email) {
			return MemberDBHandler.NewInstance().GetMemberByEmail(email);
		}

		public IEnumerable<IMember> GetMembers() {
			return MemberDBHandler.NewInstance().GetMembers();
		}

		public IMovie GetMovieByLibraryID(int ID) {
			return MovieDBHandler.NewInstance().GetMovieByLibraryID(ID);
		}

		public IEnumerable<IMovie> GetMovies() {
			return MovieDBHandler.NewInstance().GetMovies();
		}

		public IEnumerable<IMovie> GetMoviesByActor(string actor) {
			return MovieDBHandler.NewInstance().GetMoviesByActor(actor);
		}

		public IEnumerable<IMovie> GetMoviesByDirector(string director) {
			return MovieDBHandler.NewInstance().GetMoviesByDirector(director);
		}

		public IEnumerable<IMovie> GetMoviesByMediaType(string mediaType) {
			return MovieDBHandler.NewInstance().GetMoviesByMediaType(mediaType);
		}

		public IEnumerable<IMovie> GetMoviesByTitle(string title) {
			return MovieDBHandler.NewInstance().GetMoviesByTitle(title);
		}

		public int MovieAvailability(IMovie movie) {
			return MovieDBHandler.NewInstance().MovieAvailability(movie);
		}

		public void RemoveBook(IBook book) {
			throw new NotImplementedException();
		}

		public bool ReturnBook(IBook book) {
			return BookDBHandler.NewInstance().ReturnBook(book);
		}

		public bool ReturnMovie(IMovie movie, IMember member) {
			return MovieDBHandler.NewInstance().ReturnMovie(movie, member);
		}

		public void UpdateBook(IBook book) {
			DBManager.NewBookDBHandler().UpdateBook(book);
		}
	}
}
