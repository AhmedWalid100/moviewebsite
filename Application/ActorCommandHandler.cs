using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.Interfaces;

namespace MoviesProject.Application
{

    public class ActorCommandHandler : IActorCommandHandler
    {
        public IActorRepository _actorrepository;
        public ActorCommandHandler(IActorRepository actorrepository)
        {
            _actorrepository = actorrepository;
        }
        public async Task CreateActor(ActorDTO actorDTO)
        {
            Actor actor = new Actor(actorDTO.Name, actorDTO.Age, actorDTO.Bio);
            _actorrepository.AddActor(actor);
            await _actorrepository.SaveChangesAsync();
        }
        public async Task RemoveActor(int id)
        {
            var actor = _actorrepository.GetActor(id);
            _actorrepository.RemoveActor(actor);
            await _actorrepository.SaveChangesAsync();
        }
        public async Task UpdateActor(int id, ActorDTO actorDTO)
        {
            var actor = new Actor(actorDTO.Name, actorDTO.Age, actorDTO.Bio);
            _actorrepository.UpdateActor(id, actor);
            await _actorrepository.SaveChangesAsync();
        }

    }
}
