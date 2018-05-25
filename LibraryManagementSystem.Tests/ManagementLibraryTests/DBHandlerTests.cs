using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CoreLibrary.DBManagement;
using CoreLibrary.Inventory;
using System.Threading.Tasks;
using System.Diagnostics;
using CoreLibrary.Searching;
using CoreLibrary.Members;
using CoreLibrary;

namespace LibraryManagementSystem.Tests.ManagementLibraryTests
{
    public class DBHandlerTests
    {

		private bool BooksAreEqual(IBook book1, IBook book2) {
			bool areEqual = true;
			try {
				Assert.Equal(book1.LibraryID, book2.LibraryID);
				Assert.Equal(book1.Title, book2.Title);
				Assert.Equal(book1.Author, book2.Author);
				Assert.Equal(book1.Format, book2.Format);
				Assert.Equal(book1.ISBN10, book2.ISBN10);
				Assert.Equal(book1.ISBN13, book2.ISBN13);
				Assert.Equal(book1.Length, book2.Length);
				Assert.Equal(book1.Genre, book2.Genre);
				Assert.Equal(book1.PublicationYear, book2.PublicationYear);
				Assert.Equal(book1.Publisher, book2.Publisher);
				Assert.Equal(book1.Description, book2.Description);
			} catch (Exception e) {
				Debug.WriteLine(e.Message);
				areEqual = false;
			}

			return areEqual;
			
		}

		[Fact]
		public void GetBookByID_Test() {
			IBook bookExp = new Book {
				LibraryID = 0,
				Title = "Book 1",
				Author = "Author 1",
				Format = MediaFormat.PAPERBACK,
				ISBN10 = "0000000001",
				ISBN13 = "0000000000001",
				Length = 99,
				Genre = "Horror",
				PublicationYear = 2018,
				Publisher = "UnKnown",
				Description = "Book 1 Description"
			};
			IBook bookTest = SearchUtility.GetBookByLibraryID(0);

			Assert.True(this.BooksAreEqual(bookExp, bookTest));
		}
		

		[Fact]
		public void IsValidLibraryID_Test() {
			Assert.True(InventoryManager.IsValidLibraryID(0));
		}
    }
}
