using Movies.Application.Models;
using Movies.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository repository;

        public MovieService(IMovieRepository repository, CancellationToken token = default)
        {
            this.repository = repository;
        }
        public async Task<Movie> CreateAsync(Movie movie, CancellationToken token = default)
        {
            var existingMovie = await repository.GetBySlugAsync(movie.Slug, token);
            if (existingMovie != null)
            {
                throw new Exception("Movie already exists");
            }
            return await repository.CreateAsync(movie);
            
        }

        public async Task<Movie?> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {
            return await repository.DeleteByIdAsync(id, token);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(CancellationToken token = default)
        {
            return await repository.GetAllAsync(token);
        }

        public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await repository.GetByIdAsync(id, token);
        }

        public async Task<Movie?> GetBySlugAsync(string slug, CancellationToken token = default)
        {
            return await repository.GetBySlugAsync(slug, token);
        }

        public async Task<Movie?> UpdateByIdAsync(Guid id, Movie movie, CancellationToken token = default)
        {
            var existingMovie = await repository.GetByIdAsync(id, token);
            if (existingMovie == null)
            { 
                return null; 
            }
            return await repository.UpdateByIdAsync(id, movie);
        }
    }
}
