using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Payments.Events
{
    public class PaymentConfirmedEvent:INotification
    {
        public PaymentConfirmedEvent(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
