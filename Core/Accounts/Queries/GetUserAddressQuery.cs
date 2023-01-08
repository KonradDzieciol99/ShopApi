using Core.Dtos;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Accounts.Queries
{
    public class GetUserAddressQuery:IRequest<AddressDto>
    {
        public GetUserAddressQuery(ClaimsPrincipal user)
        {
            User = user;
        }

        public ClaimsPrincipal User { get; }
    }
}
