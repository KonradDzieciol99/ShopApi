using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
//using Stripe;
using System.Text.Json;

namespace Infrastructure.DataAccess
{
    public class SeedInitialData
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Moderator"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            var admin = new AppUser
            {
                UserName = "admin",
                Email = "Admin@gmail.com"
            };


            await userManager.CreateAsync(admin, "hidenPassword!");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });//dziedziela od razu 2 role czyli początkowo nie ma żadnej roli jesli jej nie nadamy

        }

        public static async Task SeedDeliveryMethod(ApplicationDbContext context)
        {
            if (!context.DeliveryMethods.Any())
            {
                var DeliveryMethods = new List<DeliveryMethod>();

                DeliveryMethods.Add(new DeliveryMethod()
                {
                    ShortName = "DHL",
                    Description = "The fastest delivery time.",
                    DeliveryTime = "1-2 Days",
                    Price = 20M
                });
                DeliveryMethods.Add(new DeliveryMethod()
                {
                    ShortName = "InPost",
                    Description = "A post office box system for collecting parcels 24/7.",
                    DeliveryTime = "2-5 Days",
                    Price = 14.5M
                });
                DeliveryMethods.Add(new DeliveryMethod()
                {
                    ShortName = "Polish Post",
                    Description = "Long delivery time but low price.",
                    DeliveryTime = "5-10 Days",
                    Price = 5M
                });

                foreach (var item in DeliveryMethods)
                {
                    context.DeliveryMethods.Add(item);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
