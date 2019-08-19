using Challenge.Core.Exceptions;
using Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Challenge.Unit.Tests.Models
{
    public class TransactionTest
    {
        public TransactionTest()
        {

        }

        [Fact]
        public void CreateEmptyTransaction_ShouldBeNotNull()
        {
            Transaction transaction = new Transaction(101, 10, "Test");

            Assert.NotNull(transaction);
        }

        [Fact]
        public void CreateTransaction_ShouldBeNotNull()
        {
            BankAccount account = new BankAccount() { AccountId = 101 };

            decimal transactionAmmount = 100;
            string transactionDescription = "Test";

            Transaction transaction = new Transaction(account.AccountId, transactionAmmount, transactionDescription);

            Assert.NotNull(transaction);
        }

        [Fact]
        public void CreateTransaction_AccountId_ShouldBeInvalid()
        {
            try
            {
                BankAccount account = new BankAccount() { AccountId = 101 };

                decimal transactionAmmount = 100;
                string transactionDescription = "´Test";

                var transaction = new Transaction(092, transactionAmmount, transactionDescription);

                Assert.NotNull(transaction);
            }
            catch (AccountNotInformedException ex)
            {
                Assert.Same(ex.Message, "Account not informed.");
            }
        }

        [Fact]
        public void CreateTransaction_Ammount_ShouldBeInvalid()
        {
            try
            {
                BankAccount account = new BankAccount() { AccountId = 101 };

                decimal transactionAmmount = 0;
                var transactionDescription = "Test";

                var transaction = new Transaction(account.AccountId, transactionAmmount, transactionDescription);

                Assert.NotNull(transaction);
            }
            catch (AccountUpdateException ex)
            {
                Assert.Same(ex.Message, "Ammount not informed.");
            }
        }

        [Fact]
        public void CreateTransaction_ShouldBeValid()
        {
            BankAccount account = new BankAccount() { AccountId = 101 };

            decimal transactionAmmount = 100;
            var transactionDescription = "Test";

            var transaction = new Transaction(account.AccountId, transactionAmmount, transactionDescription);

            Assert.Equal(transactionAmmount, transaction.Ammount);
            Assert.Equal(transactionDescription, transaction.Description);
        }

        [Fact]
        public void InformPositiveBalance_ShouldBeValid()
        {
            BankAccount account = new BankAccount() { AccountId = 101 };

            decimal transactionAmmount = 100;
            var transactionDescription = "Test";

            var transaction = new Transaction(account.AccountId, transactionAmmount, transactionDescription);

            account.Deposit(Math.Abs(transactionAmmount));

            transaction.InformCurrentBalance(account.CurrentBalance);

            account.AddTransaction(transaction);

            Assert.Equal(account.CurrentBalance, transaction.Balance);
        }

        [Fact]
        public void InformPositiveBalance_ShouldBeInvalid()
        {
            try
            {
                BankAccount account = new BankAccount() { AccountId = 101 };

                decimal transactionAmmount = 100;
                var transactionDescription = "Test";

                var transaction = new Transaction(account.AccountId, transactionAmmount, transactionDescription);

                account.Deposit(0);

                transaction.InformCurrentBalance(account.CurrentBalance);

                account.AddTransaction(transaction);

                Assert.Equal(account.CurrentBalance, transaction.Balance);
            }
            catch (AccountUpdateException ex)
            {
                Assert.Same(ex.Message, "Ammount to deposit not informed.");
            }
        }

        [Fact]
        public void InsuficientAmount_ShouldBeInvalid()
        {
            try
            {
                BankAccount account = new BankAccount() { AccountId = 101 };

                decimal transactionAmmount = -100;
                var transactionDescription = "Test";

                var transaction = new Transaction(account.AccountId, transactionAmmount, transactionDescription);

                account.Withdraw(Math.Abs(transactionAmmount));

                transaction.InformCurrentBalance(account.CurrentBalance);

                account.AddTransaction(transaction);

                Assert.Equal(account.CurrentBalance, transaction.Balance);
            }
            catch (TransactionInsuficientAmmountException ex)
            {
                Assert.Same(ex.Message, "Insuficient Ammount.");
            }
        }

        [Fact]
        public void InformNegativeBalance_ShouldBeInvalid()
        {
            try
            {
                BankAccount account = new BankAccount() { AccountId = 101 };

                decimal transactionAmmount = -100;
                var transactionDescription = "Test";

                var transaction = new Transaction(account.AccountId, transactionAmmount, transactionDescription);

                account.Withdraw(0);

                transaction.InformCurrentBalance(account.CurrentBalance);

                account.AddTransaction(transaction);

                Assert.Equal(account.CurrentBalance, transaction.Balance);
            }
            catch (AccountUpdateException ex)
            {
                Assert.Same(ex.Message, "Ammount to withdraw not informed.");
            }
        }
    }

}
