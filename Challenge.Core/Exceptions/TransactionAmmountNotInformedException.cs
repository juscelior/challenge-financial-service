using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Core.Exceptions
{
    public class TransactionAmmountNotInformedException : AccountException
    {
        public TransactionAmmountNotInformedException(string message)
            : base(message)
        {
        }
    }

}
