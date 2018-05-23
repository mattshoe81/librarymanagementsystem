using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Members
{
	public class Member : IMember {

		public int MemberID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
