using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public interface IRatingRepository
    {
        Task<Movie?> GetMovieByIdAsync(Guid id);
        Task<Rating?> GetUserRatingAsync(Guid movieId, string user);
        Task<Rating> AddRatingAsync(Rating rating);
        Task SaveChangesAsync();
    }
}
