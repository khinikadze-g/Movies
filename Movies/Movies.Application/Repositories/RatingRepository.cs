using Microsoft.EntityFrameworkCore;
using Movies.Application.Data;
using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly MoviesDbContext dbContext;

        public RatingRepository(MoviesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Rating?> GetUserRatingAsync(Guid movieId, string user)
        {
            return await dbContext.Ratings.FirstOrDefaultAsync(r => r.MovieId == movieId && r.UserName == user);
        }

        public async Task<Rating> AddRatingAsync(Rating rating)
        {
            await dbContext.Ratings.AddAsync(rating);
            await dbContext.SaveChangesAsync();
            return rating;
            
        }

        public async Task<Movie?> GetMovieByIdAsync(Guid id)
        {
            return await dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
