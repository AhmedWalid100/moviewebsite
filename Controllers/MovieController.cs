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
        public MovieController(IMovieCommandHandler moviecommandhandler, MovieDBContext dbContext)
        {
            _dbcontext=dbContext;
            _moviecommandhandler = moviecommandhandler;
        }
        // GET: api/<MovieController>
        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {

             return Ok(new string[] { "value1", "value2" });
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
            Movie movie = _dbcontext.Movies.Where(x => x.ID == id).FirstOrDefault();
            if(movie != null)
            {
                _dbcontext.Movies.Remove(movie);
                _dbcontext.SaveChanges();
                return Ok();
                
            }
            else
            {
                return NotFound();
            }
            
            //_moviecommandhandler.DeleteMovieAsync(id);
        }
    }
}
