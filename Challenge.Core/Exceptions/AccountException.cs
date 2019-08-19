using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Core.Exceptions
{
    public class AccountException : Exception
    {
        public AccountException(string message)
            : base(message)
        {
        }
    }

}
