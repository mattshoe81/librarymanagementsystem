using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;

namespace LibraryWebUI.Models
{
    public class Cart
    {
		public List<IBook> Contents { get; set; } = new List<IBook>();

		public void Empty() {
			this.Contents = new List<IBook>();
		}

		public void Add(IBook book) {
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
