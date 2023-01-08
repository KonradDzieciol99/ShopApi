using AutoMapper;
using Core.Accounts.Commands;
using Core.Accounts.Events;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Accounts.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserDto>
    {
        private readonly IMediator _mediator;
        private readonly IJwtTokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IMediator mediator , IJwtTokenService tokenService,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            this._mediator = mediator;
            this._tokenService = tokenService;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
        }

        public async Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email ==request.RegisterDto.Email)) { throw new BadRequestException("Podany Email jest już zajęty."); }

            var user = _mapper.Map<AppUser>(request.RegisterDto);

            var result = await _userManager.CreateAsync(user, request.RegisterDto.Password);

            if (!result.Succeeded) { throw new BadRequestException("Nie udało się utworzyć użytkownika"); }

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) { throw new BadRequestException("nie udało się dodać Roli"); }

            await _mediator.Publish(new UserCreatedEvent(user));

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                Name = user.UserName,
                SurName = user.SurName,
                PhoneNumber = user.PhoneNumber,
            };
        }
    }
}
