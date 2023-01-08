using Core.Accounts.Commands;
using Core.Accounts.Queries;
using Core.Dtos;
using Core.Entities;
using Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            return await _mediator.Send(new GetUserAddressQuery(User));
        }

        [Authorize]
        [HttpPut("data")]
        public async Task<ActionResult<UserDataDto>> UpdateUserData(UserDataDto userDataDto)
        {
            return await _mediator.Send(new UpdateUserDataCommand(User, userDataDto));
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            return await _mediator.Send(new UpdateUserAddressCommand(User,address));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            return await _mediator.Send(new RegisterCommand(registerDto));
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            return await _mediator.Send(new LoginCommand(loginDto));
        }
    }
}
