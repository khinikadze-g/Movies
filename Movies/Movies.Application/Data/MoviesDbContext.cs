
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Movies.Application.Models;
using System.Text.RegularExpressions;

namespace Movies.Application.Data
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {
            
        }
        public DbSet<Movie> Movies { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Movie>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.Slug = GenerateSlug(entry.Entity);
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        private string GenerateSlug(Movie movie)
        {
            var slug = Regex.Replace(movie.Title, "[^0-9A-Za-z _-]", string.Empty)
                .ToLower().Replace(" ", "-");
            return $"{slug}-{movie.YearOfRelease}";
        }
    }
}
