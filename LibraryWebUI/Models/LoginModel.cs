using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebUI.Models
{
    public class LoginModel
    {
		[Required(ErrorMessage = "Please enter your email address")]
		[RegularExpression(".+\\@.+\\..+", ErrorMessage = "The email address you entered is not valid")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please enter your password")]
		public string Password { get; set; }

		public string[] ToArray() {
			return new string[] {
				Email,
				Password
			};
		}
	}
}
