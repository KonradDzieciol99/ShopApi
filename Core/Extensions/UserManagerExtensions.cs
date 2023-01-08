using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using System.Security.Claims;
using Core.Exceptions;

namespace Core.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            string email = user.FindFirstValue(ClaimTypes.Email);

            var userFromClaim = await input.Users.Include(x => x.AppUserAddress).SingleOrDefaultAsync(x => x.Email == email);

            if (userFromClaim == null)
            {
                throw new UnauthorizedException("Can't get user");
            }
            return userFromClaim;
        }

        public static async Task<AppUser> FindByEmailFromClaimsPrinciple(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            string email = user.FindFirstValue(ClaimTypes.Email);

            var userFromClaim = await input.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (userFromClaim == null)
            {
                throw new UnauthorizedException("Can't get user");
            }
            return userFromClaim;
        }
    }
}
