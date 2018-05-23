using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Members;

namespace LibraryWebUI.Models
{
    public class MemberRepository
    {
		public IQueryable<IMember> Members { get; set; }

		public MemberRepository() {
			this.Members = CoreLibrary.Members.Members.GetMembers().AsQueryable();
		}

		public bool VerifyLogin(string emailAddress, string password) {
			bool result = false;
			if (Members.Where(member => member.Email == emailAddress).Count() > 0) {
				result = true;
				if (Members.Where(member => member.Password == password).Count() <= 0) {
					result = false;
				}
			}

			return result;
		}
	}
}
