using ControleMercadoria.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleMercadoria.API.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>

                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }

}

