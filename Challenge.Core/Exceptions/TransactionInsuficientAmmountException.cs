using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Core.Exceptions
{
    public class TransactionInsuficientAmmountException : AccountException
    {
        public TransactionInsuficientAmmountException(string message)
            : base(message)
        {
        }
    }

}
