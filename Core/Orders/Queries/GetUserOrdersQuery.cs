using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders.Queries
{
    public class GetUserOrdersQuery:IRequest<IEnumerable<OrderToReturnDto>>
    {
        public GetUserOrdersQuery(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
