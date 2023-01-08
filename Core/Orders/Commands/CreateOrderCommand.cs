using Core.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders.Commands
{
    public class CreateOrderCommand:IRequest<OrderToReturnDto>
    { 

        public CreateOrderCommand(string email, OrderDto orderDto)
        {
            Email = email;
            OrderDto = orderDto;
        }

        public string Email { get; }
        public OrderDto OrderDto { get; }
    }
}
