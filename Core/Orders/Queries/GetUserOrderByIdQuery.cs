using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders.Queries
{
    public class GetUserOrderByIdQuery : IRequest<OrderToReturnDto>
    {
        public GetUserOrderByIdQuery(string email, int id)
        {
            Email = email;
            Id = id;
        }
        public string Email { get; }
        public int Id { get; }
    }
}
