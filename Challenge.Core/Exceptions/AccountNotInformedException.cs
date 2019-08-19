using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Core.Exceptions
{
    public class AccountNotInformedException : AccountException
    {
        public AccountNotInformedException(string message)
            : base(message)
        {
        }
    }

}
