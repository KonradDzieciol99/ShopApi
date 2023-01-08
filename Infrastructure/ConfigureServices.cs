using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.IRepositories;
using Infrastructure.DataAccess;
using Infrastructure.EmailService;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShopApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection;

    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

        var from = configuration["EmailConfiguration:From"];
        var SendGrid_Key = configuration["SendGrid_Key"];
        //var smtpServer = configuration["EmailConfiguration:SmtpServer"];
        //var smtpPort = int.Parse(configuration["EmailConfiguration:Port"]);
        //var smtpUserName = configuration["EmailConfiguration:Username"];
        //var smtpPassword = configuration["EmailConfiguration:Password"];
        //var smtpClient = new SmtpClient(smtpServer, smtpPort)
        //{
        //    Credentials = new NetworkCredential(smtpUserName, smtpPassword),
        //    EnableSsl = true,
        //};
        // var dfgd = Assembly.GetCallingAssembly().GetName();
        //string projectName = Assembly.GetExecutingAssembly().FullName.Split(',')[0];
        //var fff = GetType();

        var fdsgdf = configuration.GetType();
        var dfgd = Assembly.GetExecutingAssembly().GetType();
        var c = configuration.GetType().ReflectedType;
        services.AddFluentEmail(from)
                .AddRazorRenderer()
                .AddSendGridSender(SendGrid_Key);

        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DbContextConnString"));
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidIssuer = configuration["JWT:Issuer"],
                            ValidateLifetime = true,
                        };
                    });

            services.AddIdentityCore<AppUser>(opt =>
                    {
                        opt.Password.RequireNonAlphanumeric = false;
                        opt.Password.RequireUppercase = false;
                        opt.Password.RequireDigit = false;
                    })
                    .AddRoles<AppRole>()
                    .AddRoleManager<RoleManager<AppRole>>()
                    .AddSignInManager<SignInManager<AppUser>>()
                    .AddRoleValidator<RoleValidator<AppRole>>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IValidateBasketService, ValidateBasketService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEmailService, EmailService>();

        return services;
        }
    }

