using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;

namespace LibraryWebUI.Models
{
    public class Cart
    {
		public static List<ILibraryItem> CartContents { get; set; } = new List<ILibraryItem>();

		public static void EmptyCart() {
			CartContents = new List<ILibraryItem>();
		}

		public static bool IsAlreadyInCart(int libraryID) {
			bool isInCart = false;
			foreach (ILibraryItem item in CartContents) {
				if (!isInCart) {
					isInCart = item.LibraryID == libraryID;
				}
			}

			return isInCart;
		}
    }
}
