using AutoMapper;
using MoviesProject.Application.Commands;
using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.Interfaces;

namespace MoviesProject.Application
{

    public class ActorCommandHandler : IActorCommandHandler
    {
        public IActorRepository _actorrepository;
        public IMapper _mapper;
        public ActorCommandHandler(IActorRepository actorrepository, IMapper mapper)
        {
            _actorrepository = actorrepository;
            _mapper = mapper;
        }
        public async Task CreateActor(ActorCreateCommand actorCommand)
        {
            var actor=_mapper.Map<Actor>(actorCommand);
            _actorrepository.AddActor(actor);
            await _actorrepository.SaveChangesAsync();
        }
        public async Task RemoveActor(int id)
        {
            var actor = _actorrepository.GetActor(id);
            _actorrepository.RemoveActor(actor);
            await _actorrepository.SaveChangesAsync();
        }
        public async Task UpdateActor(int id, ActorCreateCommand actorCommand)
        {
            var actor = _mapper.Map<Actor>(actorCommand);
            _actorrepository.UpdateActor(id, actor);
            await _actorrepository.SaveChangesAsync();
        }

    }
}
