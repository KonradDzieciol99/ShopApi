using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Baskets.Queries
{
    public class GetBasketByIdQuery:IRequest<CustomerBasketDto>
    {
        public GetBasketByIdQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }
}
