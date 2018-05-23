using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Members;

namespace CoreLibrary.Members
{
	public class Admin : IMember, IAdmin {

		public int MemberID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

	}
}
