using Challenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Challenge.Unit.Tests.Models
{
    public class BankAccountTest
    {
        public BankAccountTest()
        {

        }

        [Fact]
        public void CreateEmptyCheckingAccount_ShouldReturnNotNull()
        {
            BankAccount account = new BankAccount() { };

            Assert.NotNull(account);
        }

        [Fact]
        public void CreateCheckingAccount_ShouldReturnNotNull()
        {
            BankAccount account = new BankAccount() { AccountId = 101 };

            Assert.NotNull(account);
        }


        [Fact]
        public void CreateCheckingAccount_ShouldBeValid()
        {
            var accountNumber = 123;
            BankAccount account = new BankAccount() { AccountId = accountNumber };

            Assert.Equal(accountNumber, account.AccountId);
        }
    }
}
