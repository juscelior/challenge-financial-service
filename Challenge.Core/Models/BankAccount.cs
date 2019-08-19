using Challenge.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Core.Models
{
    public class BankAccount
    {
        public BankAccount()
        {
            Transactions = new List<Transaction>();
        }

        public int AccountId { get; set; }
        public decimal CurrentBalance { get; set; }

        public List<Transaction> Transactions { get; set; }

        public BankAccount AddTransaction(Transaction transaction)
        {
            this.Transactions.Add(transaction);
            return this;
        }

        public BankAccount Withdraw(decimal ammount)
        {
            if (ammount <= 0)
                throw new AccountUpdateException("Ammount to withdraw not informed.");
            else if (this.CurrentBalance < ammount)
                throw new TransactionInsuficientAmmountException("Insuficient Ammount.");

            this.CurrentBalance -= ammount;
            return this;
        }

        public BankAccount Deposit(decimal ammount)
        {
            if (ammount <= 0)
                throw new AccountUpdateException("Ammount to deposit not informed.");

            this.CurrentBalance += ammount;
            return this;
        }
    }
}
