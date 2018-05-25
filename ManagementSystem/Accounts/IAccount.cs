using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Members
{
    public interface IAccount
    {
		string FirstName { get; set; }
		string LastName { get; set; }
		string Email { get; set; }
		string Password { get; set; }
		bool Admin { get; set; }
	}
}
