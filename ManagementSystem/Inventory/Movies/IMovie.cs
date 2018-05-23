using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Inventory;

namespace CoreLibrary.Inventory
{
    public interface IMovie : ILibraryItem
    {

		double RunTimeInMinutes { get; set; }

		string Director { get; set; }

		string MainCast { get; set; }

		int LengthOfLoan { get; set; }

    }
}
