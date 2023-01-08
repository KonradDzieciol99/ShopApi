using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Payments.Commands
{
    public class ConfirmPaymentWebhookCommand:IRequest<Unit>
    {
        public ConfirmPaymentWebhookCommand(string intentId)
        {
            this.intentId = intentId;
        }

        public string intentId { get; set; }
    }
}
