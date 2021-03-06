﻿using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Inventory;
using CoreLibrary.Members;

namespace CoreLibrary.DBManagement.Handlers
{
    public interface IBookDBHandler
    {
		void RemoveBook(IBook book);

		IEnumerable<IBook> GetBooks();

		IEnumerable<IBook> GetBooksByAuthor(string author);

		IEnumerable<IBook> GetBooksByGenre(string genre);

		IEnumerable<IBook> GetBooksByPublisher(string publisher);

		IEnumerable<IBook> GetBooksByTitle(string title);

		IEnumerable<IBook> GetBooksByISBN(string ISBN);

		IBook GetBookByLibraryID(int ID);

		IEnumerable<IBook> GetCheckedOutBooks();

		int BookAvailability(IBook book);

		bool ReturnBook(IBook book);

		bool AddNewBook(IBook book);

		void UpdateBook(IBook book);

		IEnumerable<IBook> GetCheckedOutBooksByUser(string email);

		DateTime GetDueDate(int libraryID);
	}
}
