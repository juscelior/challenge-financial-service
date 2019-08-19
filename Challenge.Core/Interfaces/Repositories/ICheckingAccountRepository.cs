using Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Interfaces.Repositories
{
    public interface ICheckingAccountRepository
    {
        Task<BankAccount> GetAccountById(int accountId);
        Task<bool> Update(BankAccount account);
    }

}
