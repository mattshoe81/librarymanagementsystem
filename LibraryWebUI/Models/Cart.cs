using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;

namespace LibraryWebUI.Models
{
    public class Cart
    {
		public List<Book> Contents { get; set; } = new List<Book>();

		public void Empty() {
			this.Contents = new List<Book>();
		}

		public void Add(Book book) {
			if (!this.IsAlreadyInCart(book.LibraryID)) {
				this.Contents.Add(book);
			}
		}

		private bool IsAlreadyInCart(int libraryID) {
			bool isInCart = false;
			foreach (IBook item in Contents) {
				if (!isInCart) {
					isInCart = item.LibraryID == libraryID;
				}
			}

			return isInCart;
		}
    }
}
