using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Accounts.Commands
{
    public class LoginCommand:IRequest<UserDto>
    {
        public LoginCommand(LoginDto loginDto)
        {
            LoginDto = loginDto;
        }
        public LoginDto LoginDto { get; }
    }
}
