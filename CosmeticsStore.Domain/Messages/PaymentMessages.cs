using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Domain.Messages
{
    public static class PaymentMessages
    {
        public const string NotFound = "Payment with the given ID is not found.";
        public const string InvalidAmount = "Payment amount is invalid.";
        public const string ProviderError = "Payment provider returned an error.";
        public const string Processed = "Payment processed successfully.";
        public const string Refunded = "Payment refunded successfully.";
    }
}
