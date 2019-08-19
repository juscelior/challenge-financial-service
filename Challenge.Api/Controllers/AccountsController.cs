using System;
using System.Threading.Tasks;
using Challenge.Core.Events.Accounts.Request;
using Challenge.Core.Events.Accounts.Response;
using Challenge.Core.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Api.Controllers
{
    [Route("api/v{version:apiVersion}/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IBus _bus;

        public AccountsController(IBus bus)
        {
            _bus = bus;
        }

        [HttpGet("balance/{accountId}")]
        public async Task<IActionResult> Balance(int accountId)
        {

            Response<SearchByAccountResponse> response = await _bus.Request<SearchByAccountRequest, SearchByAccountResponse>(new SearchByAccountRequest()
            {
                AccountId = accountId
            });

            if (response.Message.Status)
            {
                return Ok(response.Message);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost("transaction")]
        public async Task<IActionResult> Transaction([FromBody] TransactionRequest request)
        {
            request.CorrelationId = Guid.NewGuid();
            Response<TransactionResponse> response = await _bus.Request<TransactionRequest, TransactionResponse>(request);

            if (response.Message.Status)
            {
                return Ok(response.Message);
            }
            else
            {
                return BadRequest(response.Message);
            }

        }
    }
}