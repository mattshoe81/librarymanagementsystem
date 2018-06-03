using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Inventory;

namespace LibraryWebUI.Models
{
    public class Cart
    {
		public static List<IBook> CartContents { get; set; } = new List<IBook>();

		public static void EmptyCart() {
			CartContents = new List<IBook>();
		}

		public static bool IsAlreadyInCart(int libraryID) {
			bool isInCart = false;
			foreach (IBook item in CartContents) {
				if (!isInCart) {
					isInCart = item.LibraryID == libraryID;
				}
			}

			return isInCart;
		}
    }
}
