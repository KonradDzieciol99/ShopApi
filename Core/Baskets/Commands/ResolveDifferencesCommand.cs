using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Baskets.Commands
{
    public class ResolveDifferencesCommand:IRequest<CustomerBasketDto>
    {
        public ResolveDifferencesCommand(CustomerBasketDto customerBasketDto, int userId)
        {
            CustomerBasketDto = customerBasketDto;
            UserId = userId;
        }

        public CustomerBasketDto CustomerBasketDto { get; }
        public int UserId { get; }
    }
}
