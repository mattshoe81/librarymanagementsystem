using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Inventory;

namespace CoreLibrary.Inventory
{
    public interface IBook : ILibraryItem
    {
		int Length { get; set; }

		string Publisher { get; set; }

		int PublicationYear { get; set; }

		string ISBN10 { get; set; }

		string ISBN13 { get; set; }

		string Genre { get; set; }

		string Description { get; set; }

		string Author { get; set; }

		int LengthOfLoan { get; set; }
    }
}
