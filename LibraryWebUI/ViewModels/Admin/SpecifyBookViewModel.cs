using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebUI.ViewModels.Admin
{
    public class SpecifyBookViewModel
    {
		public string ErrorMessage { get; set; } = "";

		public string Action { get; set; }

		public SpecifyBookViewModel() { }

		public SpecifyBookViewModel(string errorMessage) {
			this.ErrorMessage = errorMessage;
		}

    }
}
