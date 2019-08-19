using Challenge.Core.Exceptions;
using Challenge.Core.Interfaces.Repositories;
using Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.Repositories
{
    public class AccountRepository : ICheckingAccountRepository
    {
        private readonly Dictionary<int, BankAccount> repo = new Dictionary<int, BankAccount>();

        public Task<BankAccount> GetAccountById(int accountId)
        {

            if (accountId <= 0)
                throw new AccountNotFoundException("Source account id not found.");

            BankAccount currenteAccount = null;

            if (repo.ContainsKey(accountId))
            {
                currenteAccount = repo[accountId];
            }
            else
            {
                currenteAccount = new BankAccount()
                {
                    AccountId = accountId
                };
                repo.Add(accountId, currenteAccount);
            }


            return Task.FromResult(repo[accountId]);
        }

        public Task<bool> Update(BankAccount account)
        {
            bool result = false;

            try
            {
                repo[account.AccountId] = account;

                result = true;
            }
            catch (Exception ex)
            {
                throw new AccountUpdateException("Update error");
            }

            return Task.FromResult(result);
        }
    }
}
