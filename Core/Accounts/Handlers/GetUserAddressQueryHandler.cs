using AutoMapper;
using Core.Accounts.Queries;
using Core.Dtos;
using Core.Entities;
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
    public class GetUserAddressQueryHandler:IRequestHandler<GetUserAddressQuery, AddressDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public GetUserAddressQueryHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
        }

        public async Task<AddressDto> Handle(GetUserAddressQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailWithAddressAsync(request.User);
            return _mapper.Map<AddressDto>(user.AppUserAddress);
        }
    }
}
