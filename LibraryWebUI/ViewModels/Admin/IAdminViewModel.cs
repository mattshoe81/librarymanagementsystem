using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Members;

namespace LibraryWebUI.ViewModels.Admin
{
    interface IAdminViewModel
    {
		IAdmin LoggedInAdmin { get; set; }
    }
}
