﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scraper.Application.Common.Interfaces;
using Scraper.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Scraper.Infrastructure.Services;
using Scraper.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Scraper.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string wwwrootPath)
        {
            var connectionString = configuration.GetConnectionString("MariaDB");

            //DbContext
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());


            services.AddDbContext<IdentityContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            services.AddIdentity<User, Role>(options =>
            {

                // User Password Options
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                // User Username and Email Options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();


            //Scoped Services
            services.AddScoped<IAuthenticationService, AuthenticationManager>();


            return services;
        }
    }
}
