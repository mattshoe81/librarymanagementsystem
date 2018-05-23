using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Members;

namespace CoreLibrary.DBManagement.Handlers
{
    interface IMemberDBHandler
    {
		IEnumerable<IMember> GetMembers();

		IEnumerable<IMember> GetMemberByEmail(string email);

		IEnumerable<IAdmin> GetAdmins();

		IAdmin GetAdminByEmail(string email);

		bool AddNewMember(IMember member);

		bool AddNewAdmin(IAdmin admin);
	}
}
