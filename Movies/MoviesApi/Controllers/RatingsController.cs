using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Dto;
using Movies.Application.Services;
using Movies.Contracts.Requests;
using System.IdentityModel.Tokens.Jwt;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService ratingService;

        public RatingsController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }
        [Authorize]
        [HttpPost(ApiEndpoints.Movies.Rate)]
        public async Task<IActionResult> RateMovie([FromBody] CreateRatingRequest createRatingRequest)
        {
            var user = User.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;
            if (user == null)
            {
                return Unauthorized();
            }
            var rating = new CreateRatingDto
            {
                Score = createRatingRequest.Score
            };
            var (average, count) = await ratingService.RateMovieAsync(user, rating);
            return Ok(new { AverageRating = average, RatingsCount = count });
        }
    }
}
