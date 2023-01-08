using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class AppUser:IdentityUser<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
        public CustomerBasket CustomerBasket { get; set; }
        public AppUserAddress AppUserAddress { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
    }
}
