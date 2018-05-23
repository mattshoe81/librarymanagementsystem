using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Inventory;
using CoreLibrary.Members;

namespace CoreLibrary.DBManagement.Handlers
{
	class MovieDBHandler : IMovieDBHandler {

		private MovieDBHandler() {
			// Prevent Instantiation
		}

		internal static IMovieDBHandler NewInstance() {
			return new MovieDBHandler();
		}

		public bool AddNewMovie(IMovie movie) {
			throw new NotImplementedException();
		}

		public bool CheckoutMovie(IMovie movie, IMember member) {
			throw new NotImplementedException();
		}

		public IEnumerable<IMovie> GetCheckedOutMovies() {
			throw new NotImplementedException();
		}

		public IMovie GetMovieByLibraryID(int ID) {
			throw new NotImplementedException();
		}

		public IEnumerable<IMovie> GetMovies() {
			throw new NotImplementedException();
		}

		public IEnumerable<IMovie> GetMoviesByActor(string actor) {
			throw new NotImplementedException();
		}

		public IEnumerable<IMovie> GetMoviesByDirector(string director) {
			throw new NotImplementedException();
		}

		public IEnumerable<IMovie> GetMoviesByMediaType(string mediaType) {
			throw new NotImplementedException();
		}

		public IEnumerable<IMovie> GetMoviesByTitle(string title) {
			throw new NotImplementedException();
		}

		public int MovieAvailability(IMovie movie) {
			throw new NotImplementedException();
		}

		public bool ReturnMovie(IMovie movie, IMember member) {
			throw new NotImplementedException();
		}
	}
}
