using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Inventory
{
    public interface IBorrowable
    {
		int LengthOfLoan { get; set; }
    }
}
