using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Repositories;
using Movies.Application.Services;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;
using MoviesApi.Mapping;

namespace MoviesApi.Controllers
{
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpPost(ApiEndpoints.Movies.Create)]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest request, CancellationToken token)
        {
            var movie = request.MapToMovie();
            await movieService.CreateAsync(movie, token);
            var movieresponse = movie.MapToResponse();
            return CreatedAtAction(nameof(GetById), new {idOrSlug = movieresponse.Id}, movieresponse);
        }

        [HttpGet(ApiEndpoints.Movies.Get)]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] string idOrSlug, CancellationToken token)
        {
            var movie = Guid.TryParse(idOrSlug, out var id) ?
                await movieService.GetByIdAsync(id, token) :
                await movieService.GetBySlugAsync(idOrSlug, token);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie.MapToResponse());
        }

        [HttpGet(ApiEndpoints.Movies.GetAll)]
        [Authorize]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var movies = await movieService.GetAllAsync(token);
            return Ok(movies.MapToResponse());
        }
        [HttpPut(ApiEndpoints.Movies.Update)]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMovieRequest updateMovieRequest, CancellationToken token)
        {
            var movie = updateMovieRequest.MapToMovie();
            var updateMovie = await movieService.UpdateByIdAsync(id, movie, token);
            if (updateMovie == null)
            {
                return NotFound();
            }
            return Ok(updateMovie.MapToResponse());

        }
        [HttpDelete(ApiEndpoints.Movies.Delete)]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
        {
            var movie = await movieService.DeleteByIdAsync(id, token);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie.MapToResponse());
        }
    }
}
