using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Members;

namespace LibraryWebUI.ViewModels.Admin
{
	public abstract class AdminViewModel {

		public IAdmin LoggedInAdmin { get; set; } = Models.AdminRepository.LoggedInAdmin;
    }
}
