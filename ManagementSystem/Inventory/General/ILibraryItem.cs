using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Inventory
{
    public interface ILibraryItem
    {

		int LibraryID { get; set; }
		
		string Title { get; set; }

		string Format { get; set; }

		string Description { get; set; }

	}
}
