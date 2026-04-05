
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Movies.Application.Models;
using System.Text.RegularExpressions;

namespace Movies.Application.Data
{
    public class MoviesDbContext : IdentityDbContext
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var userId = "2b2bd55a-10f6-491a-85e1-29550f753be5";
            var adminId = "91714bcf-7d18-4bc4-9e20-0c80de69c68f";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = userId,
                    ConcurrencyStamp = userId,
                    NormalizedName = "User",
                    Name = "User"
                },
                new IdentityRole
                {
                    Id = adminId,
                    ConcurrencyStamp = adminId,
                    NormalizedName = "Admin",
                    Name = "Admin"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
