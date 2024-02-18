using Microsoft.AspNetCore.Mvc;
using MoviesProject.Application;
using MoviesProject.DomainLayer.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        public IActorCommandHandler _actorCommandHandler;
        public ActorController(IActorCommandHandler actorCommandHandler) {

            _actorCommandHandler = actorCommandHandler;
        }
        // GET: api/<ActorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ActorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ActorController>
        [HttpPost]
        public async Task Post([FromBody] ActorDTO actor)
        {
           await _actorCommandHandler.CreateActor(actor);
        }

        // PUT api/<ActorController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ActorDTO actor)
        {
            await _actorCommandHandler.UpdateActor(id, actor);
        }

        // DELETE api/<ActorController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _actorCommandHandler.RemoveActor(id);
        }
    }
}
