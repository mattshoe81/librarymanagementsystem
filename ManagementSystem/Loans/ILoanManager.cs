﻿using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Inventory;
using CoreLibrary.Members;

namespace CoreLibrary.Loans
{
    interface ILoanManager
    {

		DateTime LoanItem(IBook item, IAccount borrower);

		void ReturnItem(IBook item, IAccount borrower);

    }
}
