using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Inventory;

namespace CoreLibrary.Inventory 
{
    public interface IBorrowableMovie : IBorrowable, IMovie
    {
    }
}
