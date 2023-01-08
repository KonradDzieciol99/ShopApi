using Core.Interfaces;
using Core.Models;
using Core.Orders.Events;
using Core.Payments.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Payments.EventsHandlers
{
    public class PaymentConfirmedEventHandler : INotificationHandler<PaymentConfirmedEvent>
    {
        private readonly IEmailService _emailService;

        public PaymentConfirmedEventHandler(IEmailService emailService)
        {
            this._emailService = emailService;
        }

        public async Task Handle(PaymentConfirmedEvent notification, CancellationToken cancellationToken)
        {
            var message = new Message(new List<string>() { notification.Order.BuyerEmail }, "Confirmed Order", "Confirmed Order Content.");
            await _emailService.SendConfirmPaymentEmail(message, notification.Order);
            return;
        }
    }
}
