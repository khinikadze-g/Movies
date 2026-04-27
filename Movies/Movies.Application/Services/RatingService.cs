
using Movies.Application.Dto;
using Movies.Application.Models;
using Movies.Application.Repositories;

namespace Movies.Application.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }
        public async Task<(double average, int count)> RateMovieAsync(string user, CreateRatingDto rating)
        {
            
            var movie = await ratingRepository.GetMovieByIdAsync(rating.MovieId);
            if (movie == null)
            {
                throw new KeyNotFoundException("Movie not found");
            } 
            var existingRating = await ratingRepository.GetUserRatingAsync(rating.MovieId, user);

            if (existingRating == null)
            { 
                var newRating = new Rating
                {
                    Score = rating.Score,
                    UserName = user,
                };
                await ratingRepository.AddRatingAsync(newRating);

                    movie.AverageRating =
                    ((movie.AverageRating * movie.RatingCount) + rating.Score)
                    / (movie.RatingCount + 1);
                movie.RatingCount++;
            }
            else
            {
                var oldScore = existingRating.Score;
                existingRating.Score = rating.Score;

                movie.AverageRating =
                    ((movie.AverageRating * movie.RatingCount) - oldScore + rating.Score)
                    / movie.RatingCount;
            }

            await ratingRepository.SaveChangesAsync();
            return (movie.AverageRating, movie.RatingCount);
        }
    }
    
}
