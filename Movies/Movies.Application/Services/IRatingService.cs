
using Movies.Application.Dto;
using Movies.Application.Models;

namespace Movies.Application.Services
{
    public interface IRatingService
    {
        Task<(double average, int count)> RateMovieAsync(string user, CreateRatingDto rating);
    }
}
