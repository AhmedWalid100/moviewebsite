using MoviesProject.Application;
using MoviesProject.Application.Commands;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IActorCommandHandler
    {
        Task CreateActor(ActorCreateCommand actorCommand);
        Task RemoveActor(int id);
        Task UpdateActor(int id, ActorCreateCommand actorCommand);
    }
}