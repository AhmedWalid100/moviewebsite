using Microsoft.AspNetCore.Mvc;
using MoviesProject.Application;
using MoviesProject.Application.Commands;
using MoviesProject.DomainLayer.Interfaces;

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
        public IActionResult Get()
        {
            return Ok(_actorQueryHandler.GetAllActors());
        }

        // GET api/<ActorController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_actorQueryHandler.GetActorById(id));
        }

        // POST api/<ActorController>
        [HttpPost]
        public async Task Post([FromBody] ActorCreateCommand actorCommand)
        {
           await _actorCommandHandler.CreateActor(actorCommand);
        }

        // PUT api/<ActorController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ActorCreateCommand actorCommand)
        {
            await _actorCommandHandler.UpdateActor(id, actorCommand);
        }

        // DELETE api/<ActorController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _actorCommandHandler.RemoveActor(id);
        }
    }
}
