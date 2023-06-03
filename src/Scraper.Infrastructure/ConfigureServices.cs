using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scraper.Application.Common.Interfaces;
using Scraper.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;


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


            return services;
        }
    }
}
