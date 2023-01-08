using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Baskets.Commands
{
    public class ValidateBasketCommand:IRequest<CustomerBasketDto>
    {
        public ValidateBasketCommand(CustomerBasketDto basket)
        {
            Basket = basket;
        }

        public CustomerBasketDto Basket { get; }
    }
}
