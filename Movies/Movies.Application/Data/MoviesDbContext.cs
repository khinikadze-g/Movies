
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Movies.Application.Models;

namespace Movies.Application.Data
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {
            
        }
        public DbSet<Movie> Movies { get; set; }
    }
}
