using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.DBManagement;

namespace CoreLibrary.Members
{
    public static class Members
    {
		public static IEnumerable<IMember> GetMembers() {
			return DBManager.NewMemberDBHandler().GetMembers();
		}
    }
}
