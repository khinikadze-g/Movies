using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;
using MoviesApi.Mapping;

namespace MoviesApi.Controllers
{
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ImovieRepository movieRepository;

        public MoviesController(ImovieRepository movierepository)
        {
            this.movieRepository = movierepository;
        }

        [HttpPost(ApiEndpoints.Movies.Create)]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
        {
            var movie = request.MapToMovie();
            await movieRepository.CreateAsync(movie);
            var movieresponse = movie.MapToResponse();
            return CreatedAtAction(nameof(GetById), new {id = movieresponse.Id}, movieresponse);
        }

        [HttpGet(ApiEndpoints.Movies.Get)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var movie = await movieRepository.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie.MapToResponse());
        }

        [HttpGet(ApiEndpoints.Movies.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var movies = await movieRepository.GetAllAsync();
            return Ok(movies.MapToResponse());
        }
        [HttpPut(ApiEndpoints.Movies.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMovieRequest updateMovieRequest)
        {
            var movie = updateMovieRequest.MapToMovie();
            var updateMovie = await movieRepository.UpdateByIdAsync(id, movie);
            if (updateMovie == null)
            {
                return NotFound();
            }
            return Ok(updateMovie.MapToResponse());

        }
        [HttpDelete(ApiEndpoints.Movies.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var movie = await movieRepository.DeleteByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie.MapToResponse());
        }
    }
}
