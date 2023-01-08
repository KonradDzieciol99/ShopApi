using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders.Events
{
    public class OrderCreatedEvent : INotification
    {
        public OrderCreatedEvent(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
