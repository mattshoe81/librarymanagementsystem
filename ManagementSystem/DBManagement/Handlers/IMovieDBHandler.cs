using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Inventory;
using CoreLibrary.Members;

namespace CoreLibrary.DBManagement.Handlers
{
    interface IMovieDBHandler
    {
		IEnumerable<IMovie> GetMovies();

		int MovieAvailability(IMovie movie);

		IEnumerable<IMovie> GetMoviesByDirector(string director);

		IEnumerable<IMovie> GetMoviesByActor(string actor);

		IEnumerable<IMovie> GetMoviesByMediaType(string mediaType);

		IEnumerable<IMovie> GetMoviesByTitle(string title);

		IMovie GetMovieByLibraryID(int ID);

		IEnumerable<IMovie> GetCheckedOutMovies();

		bool CheckoutMovie(IMovie movie, IMember member);

		bool ReturnMovie(IMovie movie, IMember member);

		bool AddNewMovie(IMovie movie);
	}
}
