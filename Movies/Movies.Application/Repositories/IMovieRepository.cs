
using Movies.Application.Models;

namespace Movies.Application.Repositories
{
    public interface IMovieRepository

    {
        Task<Movie> CreateAsync(Movie movie, CancellationToken token = default);
        Task<IEnumerable<Movie>> GetAllAsync(CancellationToken token = default);
        Task<Movie?> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<Movie?> GetBySlugAsync(string slug, CancellationToken token = default);
        Task<Movie?> UpdateByIdAsync(Guid id, Movie movie, CancellationToken token = default);
        Task<Movie?> DeleteByIdAsync(Guid id, CancellationToken token = default);

    }
}
