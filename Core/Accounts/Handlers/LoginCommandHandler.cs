using AutoMapper;
using Core.Accounts.Commands;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Accounts.Handlers
{
    public class LoginCommandHandler:IRequestHandler<LoginCommand, UserDto>
    {
        private readonly IJwtTokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public LoginCommandHandler(IJwtTokenService tokenService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            this._tokenService = tokenService;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
        }

        public async Task<UserDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.Email == request.LoginDto.Email);

            if (user == null) { throw new BadRequestException("Błędne Dane Logowania"); };

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, request.LoginDto.Password, false);

            if (!result.Succeeded) { throw new BadRequestException("Błędne Hasło"); };

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
