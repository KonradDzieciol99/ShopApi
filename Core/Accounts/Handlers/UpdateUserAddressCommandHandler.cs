using AutoMapper;
using Core.Accounts.Commands;
using Core.Accounts.Queries;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Accounts.Handlers
{
    public class UpdateUserAddressCommandHandler : IRequestHandler<UpdateUserAddressCommand, AddressDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public UpdateUserAddressCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
        }
        public async Task<AddressDto> Handle(UpdateUserAddressCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailWithAddressAsync(request.User);

            user.AppUserAddress = _mapper.Map<AppUserAddress>(request.AddressDto);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded){return _mapper.Map<AddressDto>(user.AppUserAddress); }

            throw new BadRequestException("Address update failed");
        }
    }
}
