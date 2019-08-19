using Challenge.Core.Events.Accounts.Request;
using Challenge.Core.Events.Accounts.Response;
using Challenge.Core.Interfaces.Repositories;
using Challenge.Core.Models;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Challenge.Api.Consumer.Accounts
{
    public class SearchByAccountConsumer : IConsumer<SearchByAccountRequest>
    {
        private readonly ICheckingAccountRepository checkingAccountRepository;

        public SearchByAccountConsumer(ICheckingAccountRepository checkingAccountRepository)
        {
            this.checkingAccountRepository = checkingAccountRepository;
        }

        public async Task Consume(ConsumeContext<SearchByAccountRequest> context)
        {
            try
            {
                BankAccount account = await checkingAccountRepository.GetAccountById(context.Message.AccountId);


                await context.RespondAsync<SearchByAccountResponse>(new SearchByAccountResponse() { Status = true, Account = account });
            }
            catch (Exception ex)
            {
                await context.RespondAsync<SearchByAccountResponse>(new SearchByAccountResponse() { Status = false, Message = ex.Message });
            }
        }

    }
}
