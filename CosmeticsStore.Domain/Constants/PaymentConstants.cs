using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Constants
{
    public static class PaymentConstants
    {
        public const int TransactionIdMaxLength = 200;
        public const int ProviderNameMaxLength = 100;
        public const decimal MaxPaymentAmount = 10_000_000m;

        public const string StatusPending = "Pending";
        public const string StatusProcessed = "Processed";
        public const string StatusFailed = "Failed";
        public const string StatusRefunded = "Refunded";
    }
}
