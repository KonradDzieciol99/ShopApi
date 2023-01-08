using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Accounts.Commands
{
    public class RegisterCommand: IRequest<UserDto>
    {
        public RegisterCommand(RegisterDto registerDto)
        {
            RegisterDto = registerDto;
        }
        public RegisterDto RegisterDto { get; }
    }
}
