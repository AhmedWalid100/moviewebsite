using Microsoft.AspNetCore.Mvc;
using MoviesProject.Application;
using MoviesProject.Application.Commands;
using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.Interfaces;
using MoviesProject.Infrastructure.DBContext;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        public IMovieCommandHandler _moviecommandhandler;
        public MovieDBContext _dbcontext;
        public IMovieQueryHandler _moviequeryhandler;
        public MovieController(IMovieCommandHandler moviecommandhandler, MovieDBContext dbContext, 
            IMovieQueryHandler moviequeryhandler)
        {
            _dbcontext = dbContext;
            _moviecommandhandler = moviecommandhandler;
            _moviequeryhandler = moviequeryhandler;
        }
        // GET: api/<MovieController>
        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies= await _moviequeryhandler.GetAllMovies();
             return Ok(movies);
        }

        // GET api/<MovieController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var movieDTO=_moviequeryhandler.GetMovieByID(id);
            return Ok(movieDTO);
        }

        // POST api/<MovieController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovieCreateCommand movieCommand)
        {
            await _moviecommandhandler.CreateMovieAsync(movieCommand);
            return Ok();
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MovieCreateCommand movie)
        {
            await _moviecommandhandler.UpdateMovieDetails(id , movie);
            return Ok("Done");
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _moviecommandhandler.DeleteMovieAsync(id);
            return Ok("Done");
            //_moviecommandhandler.DeleteMovieAsync(id);
        }
        [HttpPut("AddActorToMovie")]
        public async Task<IActionResult> AddActorToMovie(int movieID, int actorID)
        {
            await _moviecommandhandler.AddMovieActor(movieID, actorID);
            return Ok("Done");
        }
        [HttpGet("GetMovieActors/{id}")]
        public IActionResult GetActorsByMovieID(int id)
        {
            var actors = _moviequeryhandler.GetMovieActors(id);
            return Ok(actors);
        }
        [HttpDelete("DeleteMovieActor")]
        public async Task DeleteMovieActor(int movieID, int actorID)
        {
            await _moviecommandhandler.RemoveMovieActor(movieID, actorID);
            //return Ok("Done");
        }

    }
}
