using MoviesProject.Application;
using MoviesProject.Application.Commands;
using MoviesProject.Application.ResponseModels;
using MoviesProject.DomainLayer.Aggregates;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IActorCommandHandler
    {
        Task<CreateResponse<Actor>> CreateActor(ActorCreateCommand actorCommand);
        Task RemoveActor(int id);
        Task UpdateActor(int id, ActorCreateCommand actorCommand);

    }
}