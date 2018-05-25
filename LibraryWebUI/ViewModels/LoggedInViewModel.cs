﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Members;

namespace LibraryWebUI.ViewModels
{
	public abstract class LoggedInViewModel {

		public IAccount LoggedInAdmin { get; set; } = Models.AccountRepository.LoggedInAccount;
    }
}
