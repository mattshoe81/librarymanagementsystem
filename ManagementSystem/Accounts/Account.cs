using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Members
{
	public class Account : IAccount {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public bool Admin { get; set; }
	}
}
