using AutoMapper;
using Core.Accounts.Commands;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Accounts.Handlers
{
    public class UpdateUserDataCommandHandler:IRequestHandler<UpdateUserDataCommand, UserDataDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public UpdateUserDataCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
        }

        public async Task<UserDataDto> Handle(UpdateUserDataCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(request.User);

            user.PhoneNumber = request.UserDataDto.PhoneNumber ?? user.PhoneNumber;
            user.SurName = request.UserDataDto.SurName ?? user.SurName;
            user.Email = request.UserDataDto.Email ?? user.Email;
            user.UserName = request.UserDataDto.Name ?? user.UserName;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) { return _mapper.Map<UserDataDto>(user);};

            throw new BadRequestException("Nie udało się zaktualizować danych");
        }
    }
}
