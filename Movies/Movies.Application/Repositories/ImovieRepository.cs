
using Movies.Application.Models;

namespace Movies.Application.Repositories
{
    public interface ImovieRepository
    {
        Task<Movie> CreateAsync(Movie movie);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(Guid id);
        Task<Movie?> UpdateByIdAsync(Guid id, Movie movie);
        Task<Movie?> DeleteByIdAsync(Guid id);

    }
}
