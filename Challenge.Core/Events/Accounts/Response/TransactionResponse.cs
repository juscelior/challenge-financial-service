using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Core.Events.Accounts.Response
{
    public class TransactionResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
