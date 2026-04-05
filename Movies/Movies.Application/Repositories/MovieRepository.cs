
using Microsoft.EntityFrameworkCore;
using Movies.Application.Data;
using Movies.Application.Models;

namespace Movies.Application.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesDbContext dbContext;

        public MovieRepository(MoviesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Movie> CreateAsync(Movie movie, CancellationToken token = default)
        {
            await dbContext.Movies.AddAsync(movie, token);
            await dbContext.SaveChangesAsync(token);
            return movie;
        }

        public async Task<Movie?> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {
            var existingMovie = await dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id, token);
            if (existingMovie == null)
            {
                return null;
            }
            dbContext.Movies.Remove(existingMovie);
            await dbContext.SaveChangesAsync(token);
            return existingMovie;

        }

        public async Task<IEnumerable<Movie>> GetAllAsync(CancellationToken token = default)
        {
            return await dbContext.Movies.ToListAsync(token);
        }

        public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await dbContext.Movies.FirstOrDefaultAsync(x=> x.Id == id, token);
        }

        public async Task<Movie?> GetBySlugAsync(string slug, CancellationToken token = default)
        {
            return await dbContext.Movies.FirstOrDefaultAsync(x => x.Slug == slug, token);
        }

        public async Task<Movie?> UpdateByIdAsync(Guid id, Movie movie, CancellationToken token = default)
        {
            var existingMovie = await dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id, token);
            if (existingMovie == null)
            {
                return null;
            }
            existingMovie.Title = movie.Title;
            existingMovie.YearOfRelease = movie.YearOfRelease;
            existingMovie.Genres = movie.Genres;
            await dbContext.SaveChangesAsync(token);
            return existingMovie;

        }
    }
}
