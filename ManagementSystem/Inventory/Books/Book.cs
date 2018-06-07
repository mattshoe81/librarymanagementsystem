using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Inventory
{
	public class Book : IBorrowableBook {

		public int LibraryID { get; set; } = -1;

		public string Title { get; set; } = "<Unknown>";

		public string Author { get; set; } = "<Unknown>";

		public string Format { get; set; } = "<Unknown>";

		public string ISBN10 { get; set; } = "<Unknown>";

		public string ISBN13 { get; set; } = "<Unknown>";

		public int Length { get; set; } = -1;

		public string Genre { get; set; } = "<Unknown>";

		public string Publisher { get; set; } = "<Unknown>";

		public int PublicationYear { get; set; } = -1;

		public string Description { get; set; } = "<Unknown>";

		public int LengthOfLoan { get; set; } = 7;

		public byte[] ImageBytes { get; set; } = null;

		public bool InStock { get; set; }

		public override bool Equals(object obj) {
			if (obj == this) {
				return true;
			}
			if (!(obj is IBook)) {
				return false;
			} else {
				IBook book = (IBook) obj;
				return book.LibraryID == this.LibraryID;
			}
		}
	}
}
