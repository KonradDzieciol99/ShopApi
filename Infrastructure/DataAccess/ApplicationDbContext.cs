using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,int,
                                IdentityUserClaim<int>,AppUserRole,IdentityUserLogin<int>,
                                IdentityRoleClaim<int>,IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {}
        public DbSet<Product> Product { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<CustomerBasket> CustomerBaskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasOne<CustomerBasket>(s => s.CustomerBasket)
                .WithOne(ad => ad.AppUser)
                .HasForeignKey<CustomerBasket>(n => n.AppUserId);

            builder.Entity<AppUser>()
            .HasOne(a => a.AppUserAddress)
            .WithOne(a => a.AppUser)
            .HasForeignKey<AppUserAddress>(c => c.AppUserId);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId);

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId); 

            builder.Entity<Product>(p =>
            {
                p.HasKey(k => k.Id);

                p.HasMany(p => p.Photos)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId);

                p.HasOne(p => p.CategoryOfProduct)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryOfProductId);

                p.HasOne(p => p.BrandOfProduct)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.BrandOfProductId);

                p.Property(p=>p.Price).HasColumnType("decimal(18,2)");
                p.Property(p => p.cutPrice).HasColumnType("decimal(18,2)");
            });

            builder.Entity<Photo>(p =>
            {
                p.HasKey(p => p.Id);
            });


            builder.Entity<CustomerBasket>(opt =>
            {
                opt.Property(p => p.ShippingPrice).HasColumnType("decimal(18,2)");

            });


            builder.Entity<BasketItem>(opt =>
            {
                opt.HasKey(x => new { x.ProductId, x.CustomerBasketId });

                opt.HasOne(s => s.CustomerBasket)
                .WithMany(ad => ad.BasketItems)
                .HasForeignKey(n => n.CustomerBasketId);

                opt.HasOne(s=>s.Product)
                .WithMany(ad => ad.BasketItem)
                .HasForeignKey(n => n.ProductId);

                opt.Property(p => p.Price).HasColumnType("decimal(18,2)");

            });

            builder.Entity<OrderItem>(opt =>
            {
                opt.HasOne(s => s.Order)
                .WithMany(ad => ad.OrderItems)
                .HasForeignKey(n => n.OrderId);

                opt.HasOne(s => s.Product)
                .WithMany(ad => ad.OrderItems)
                .HasForeignKey(n => n.ProductItemId);

                opt.Property(p => p.Price).HasColumnType("decimal(18,2)");

            });

            builder.Entity<Order>(opt =>
            {
                opt.OwnsOne(o => o.ShipToAddress, a =>
                {
                    a.WithOwner();
                });
                opt.Property(p => p.Subtotal).HasColumnType("decimal(18,2)");

            });

            builder.Entity<DeliveryMethod>(opt =>
            {
                opt.Property(p => p.Price).HasColumnType("decimal(18,2)");
            });
        }

    }
}
