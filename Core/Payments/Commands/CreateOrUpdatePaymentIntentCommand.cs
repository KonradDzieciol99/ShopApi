using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Payments.Commands
{
    public class CreateOrUpdatePaymentIntentCommand:IRequest<CustomerBasketDto>
    {
        public CreateOrUpdatePaymentIntentCommand(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }
}
