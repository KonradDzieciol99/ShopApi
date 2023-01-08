using Core.Exceptions;
using System.Security.Claims;


namespace Core.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUserEmailFromClaims(this ClaimsPrincipal claims)
        {
            string email = claims.FindFirst(ClaimTypes.Email)?.Value ?? throw new UnauthorizedException("Can't get user Email"); ;

            return email;

        }

        public static int GetUserIdFromClaims(this ClaimsPrincipal claims)
        {
            int id;
            if(int.TryParse(claims.FindFirst(ClaimTypes.NameIdentifier)?.Value, out id))
            {
                return id;
            }
            throw new UnauthorizedException("Can't get user Id");
        }
    }
}
