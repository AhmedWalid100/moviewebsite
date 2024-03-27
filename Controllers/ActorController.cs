using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesProject.Application;
using MoviesProject.Application.Commands;
using MoviesProject.DomainLayer.Interfaces;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        public IActorCommandHandler _actorCommandHandler;
        public IActorQueryHandler _actorQueryHandler;
        public ActorController(IActorCommandHandler actorCommandHandler, IActorQueryHandler actorQueryHandler = null)
        {

            _actorCommandHandler = actorCommandHandler;
            _actorQueryHandler = actorQueryHandler;
        }
        // GET: api/<ActorController>
        [HttpGet]
        public IActionResult Get([Range(1, int.MaxValue)] int page = 1, int pageSize = 5,
            string? searchName = null, string orderColumn = "id", string orderBy = "asc")
        {
            return Ok(_actorQueryHandler.GetAllActors(page, pageSize, searchName, orderColumn, orderBy));
        }

        // GET api/<ActorController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_actorQueryHandler.GetActorById(id));
        }

        [Authorize(Roles = "Admin")]
        // POST api/<ActorController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ActorCreateCommand actorCommand)
        {
           var actor= await _actorCommandHandler.CreateActor(actorCommand);
            return Ok(actor);
        }

        // PUT api/<ActorController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ActorCreateCommand actorCommand)
        {
            await _actorCommandHandler.UpdateActor(id, actorCommand);
        }

        // DELETE api/<ActorController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _actorCommandHandler.RemoveActor(id);
        }
        [HttpGet("GetMoviesByActorID/{id}")]
        public  IActionResult GetMoviesByActorID(int id)
        {
            var movies =  _actorQueryHandler.GetMoviesByActorID(id);
            return Ok(movies);
        }
    }
}
