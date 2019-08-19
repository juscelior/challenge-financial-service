using Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Core.Events.Accounts.Response
{
    public class SearchByAccountResponse
    {
        public bool Status { get; set; }
        public BankAccount Account { get; set; }
        public string Message { get; set; }
    }
}
