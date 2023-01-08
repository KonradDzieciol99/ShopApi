using MediatR;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Payments.Commands
{
    public class ConfirmOrRejectPaymentWebhookCommand : IRequest<Unit>
    {
        public ConfirmOrRejectPaymentWebhookCommand(string json, StringValues stripeSignature)
        {
            Json = json;
            StripeSignature = stripeSignature;
        }

        public string Json { get; }
        public StringValues StripeSignature { get; }
    }
}
