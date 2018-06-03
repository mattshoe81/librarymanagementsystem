using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Inventory
{
	class Movie : IBorrowableMovie {

		public int LengthOfLoan { get; set; } = 7;
		public double RunTimeInMinutes { get; set; }
		public string Director { get; set; }
		public string MainCast { get; set; }
		public string Format { get; set; } = "<Unknown>";
		public int LibraryID { get; set; }
		public string Title { get; set; }
		public string Description { get; set; } = "<Unknown>";
		public byte[] ImageBytes { get; set; } = null;
		public bool InStock { get; set; }
	}
}
