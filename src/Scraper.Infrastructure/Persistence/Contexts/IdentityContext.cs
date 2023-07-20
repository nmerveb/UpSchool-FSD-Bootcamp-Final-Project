﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scraper.Domain.Entities;
using Scraper.Domain.Identity;
using System.Reflection;

namespace Scraper.Infrastructure.Persistence.Contexts
{
    public class IdentityContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {


        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Ignores
            modelBuilder.Ignore<Order>();
            modelBuilder.Ignore<Product>();
            modelBuilder.Ignore<OrderEvent>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
