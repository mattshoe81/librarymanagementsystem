using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CoreLibrary.Inventory;
using CoreLibrary.Members;
using CoreLibrary.DBManagement.Handlers;

namespace CoreLibrary.DBManagement {
	internal interface IDBHandler : IBookDBHandler, IMovieDBHandler, IMemberDBHandler {	

	}
}