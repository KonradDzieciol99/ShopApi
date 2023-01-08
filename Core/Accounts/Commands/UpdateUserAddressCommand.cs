using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Accounts.Commands
{
    public class UpdateUserAddressCommand:IRequest<AddressDto>
    {
        public UpdateUserAddressCommand(ClaimsPrincipal user, AddressDto addressDto)
        {
            User = user;
            AddressDto = addressDto;
        }

        public ClaimsPrincipal User { get; }
        public AddressDto AddressDto { get; }
    }
}
