using Challenge.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Core.Models
{
    public class Transaction
    {
        public Transaction(int accountId, decimal ammount, string description = null)
        {
            if (accountId <= 0)
                throw new AccountNotInformedException("Account id not informed.");

            if (ammount == 0)
                throw new AccountUpdateException("Ammount not informed.");

            this.AccountId = accountId;
            this.Description = description;
            this.Ammount = ammount;
            this.TransactionDate = DateTime.UtcNow;
        }

        public int AccountId { get; protected set; }
        public BankAccount CheckingAccount { get; set; }
        public DateTime TransactionDate { get; protected set; }
        public string Description { get; protected set; }
        public decimal Ammount { get; protected set; }
        public decimal Balance { get; protected set; }

        public Transaction InformCurrentBalance(decimal balance)
        {
            this.Balance = balance;
            return this;
        }
    }

}
