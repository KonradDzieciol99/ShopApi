using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmPaymentEmail(Message message, Order order);
        Task SendPendingOrderEmail(Message message, Order order);
        //void SendRejectPaymentEmail(Message message, Order order);
        Task SendWelcomeEmail(Message message);
        Task SendContactMessage(Message message); 
    }
}
