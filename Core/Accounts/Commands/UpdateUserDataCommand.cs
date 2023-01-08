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
    public class UpdateUserDataCommand :IRequest<UserDataDto>
    {
        public UpdateUserDataCommand(ClaimsPrincipal user, UserDataDto userDataDto)
        {
            User = user;
            UserDataDto = userDataDto;
        }

        public ClaimsPrincipal User { get; }
        public UserDataDto UserDataDto { get; }
    }
}
