using Challenge.Core.Events.Accounts.Request;
using Challenge.Core.Events.Accounts.Response;
using Challenge.Core.Exceptions;
using Challenge.Core.Interfaces.Repositories;
using Challenge.Core.Models;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Api.Handler.Accounts
{
    public class TransactionConsumer : IConsumer<TransactionRequest>
    {
        private readonly ICheckingAccountRepository checkingAccountRepository;

        public TransactionConsumer(ICheckingAccountRepository checkingAccountRepository)
        {
            this.checkingAccountRepository = checkingAccountRepository;
        }

        public async Task Consume(ConsumeContext<TransactionRequest> context)
        {
            try
            {
                bool result = false;

                result = await WithdrawAmmount(context.Message.OrignAccountId, context.Message.Ammount) && await DepositAmmount(context.Message.DestAccountId, context.Message.Ammount);

                await context.RespondAsync<TransactionResponse>(new TransactionResponse() { Status = result });
            }
            catch (Exception ex)
            {
                await context.RespondAsync<TransactionResponse>(new TransactionResponse() { Status = false, Message = ex.Message });
            }
        }

        public async Task<bool> WithdrawAmmount(int accountId, decimal ammount)
        {
            var account = await checkingAccountRepository.GetAccountById(accountId);

            if (account == null)
                throw new AccountNotFoundException("Source account id not found.");

            account.Withdraw(ammount);

            var transaction = new Transaction(account.AccountId, (Math.Abs(ammount) * (-1)));
            transaction.InformCurrentBalance(account.CurrentBalance);

            account.AddTransaction(transaction);
            await checkingAccountRepository.Update(account);

            return true;
        }

        public async Task<bool> DepositAmmount(int accountNumber, decimal ammount)
        {
            var account = await checkingAccountRepository.GetAccountById(accountNumber);

            if (account == null)
                throw new AccountNotFoundException("Destination account id not found.");

            account.Deposit(ammount);

            var transaction = new Transaction(account.AccountId, (Math.Abs(ammount)));
            transaction.InformCurrentBalance(account.CurrentBalance);

            account.AddTransaction(transaction);
            await checkingAccountRepository.Update(account);

            return true;
        }

    }
}
