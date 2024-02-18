using Microsoft.AspNetCore.Mvc;
using MoviesProject.Application;
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
            return Ok("value");
        }

        // POST api/<MovieController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovieDTO movieDTO)
        {
            await _moviecommandhandler.CreateMovieAsync(movieDTO);
            return Ok();
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MovieDTO movieDTO)
        {
            await _moviecommandhandler.UpdateMovieDetails(id , movieDTO);
            return Ok("Done");
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _moviecommandhandler.DeleteMovieAsync(id);
            return Ok("Done");
            //_moviecommandhandler.DeleteMovieAsync(id);
        }
    }
}
