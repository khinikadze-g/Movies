using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Data;
using Movies.Application.Repositories;
using Movies.Application.Services;

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
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            
        
            return services;
        }
    }
}
