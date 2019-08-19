using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Core.Exceptions
{
    public class AccountUpdateException : AccountException
    {
        public AccountUpdateException(string message)
            : base(message)
        {
        }
    }

}
