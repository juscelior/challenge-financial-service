using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Core.Exceptions
{
    public class AccountNotFoundException : AccountException
    {
        public AccountNotFoundException(string message)
            : base(message)
        {
        }
    }

}
