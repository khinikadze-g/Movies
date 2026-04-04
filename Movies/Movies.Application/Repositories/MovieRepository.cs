
using Microsoft.EntityFrameworkCore;
using Movies.Application.Data;
using Movies.Application.Models;

namespace Movies.Application.Repositories
{
    public class MovieRepository : ImovieRepository
    {
        private readonly MoviesDbContext dbContext;

        public MovieRepository(MoviesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Movie> CreateAsync(Movie movie)
        {
            await dbContext.Movies.AddAsync(movie);
            await dbContext.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie?> DeleteByIdAsync(Guid id)
        {
            var existingMovie = await dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if (existingMovie == null)
            {
                return null;
            }
            dbContext.Movies.Remove(existingMovie);
            await dbContext.SaveChangesAsync();
            return existingMovie;

        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await dbContext.Movies.ToListAsync();
        }

        public async Task<Movie?> GetByIdAsync(Guid id)
        {
            return await dbContext.Movies.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Movie?> UpdateByIdAsync(Guid id, Movie movie)
        {
            var existingMovie = await dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if (existingMovie == null)
            {
                return null;
            }
            existingMovie.Title = movie.Title;
            existingMovie.YearOfRelease = movie.YearOfRelease;
            existingMovie.Genres = movie.Genres;
            await dbContext.SaveChangesAsync();
            return existingMovie;

        }
    }
}
