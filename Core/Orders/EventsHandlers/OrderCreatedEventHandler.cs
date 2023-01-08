using Core.Interfaces;
using Core.Models;
using Core.Orders.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders.EventsHandlers
{
    public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        private readonly IEmailService _emailService;

        public OrderCreatedEventHandler(IEmailService emailService)
        {
            this._emailService = emailService;
        }
        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {            
            var message = new Message(new List<string>() { notification.Order.BuyerEmail }, "Pending Order", "Pending Order Content.");
            await _emailService.SendPendingOrderEmail(message, notification.Order);
        }
    }
}
