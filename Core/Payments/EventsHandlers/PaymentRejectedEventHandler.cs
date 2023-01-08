using Core.Interfaces;
using Core.Models;
using Core.Payments.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Payments.EventsHandlers
{
    public class PaymentRejectedEventHandler:INotificationHandler<PaymentRejectedEvent>
    {
        private readonly IEmailService _emailService;

    public PaymentRejectedEventHandler(IEmailService emailService)
    {
        this._emailService = emailService;
    }

        public Task Handle(PaymentRejectedEvent notification, CancellationToken cancellationToken)
        {

            var message = new Message(new List<string>() { notification.Order.BuyerEmail }, "Rejected Order", "Rejected Order Content.");
            //_emailService.SendRejectPaymentEmail(message, notification.Order);
            return Task.CompletedTask;
        }
    }
}
