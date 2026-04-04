using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Data;

namespace Movies.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<MoviesDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MovieConnectionString"));
            });
            
        
            return services;
        }
    }
}
