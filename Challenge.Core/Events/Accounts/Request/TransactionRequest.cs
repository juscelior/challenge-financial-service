using Challenge.Core.Models;
using System;

namespace Challenge.Core.Events.Accounts.Request
{
    public class TransactionRequest
    {
        public Guid CorrelationId { get; set; }
        public int OrignAccountId { get; set; }
        public int DestAccountId { get; set; }

        public decimal Ammount { get; set; }
    }
}
