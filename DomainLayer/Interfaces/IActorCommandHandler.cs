using MoviesProject.Application;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IActorCommandHandler
    {
        Task CreateActor(ActorDTO actorDTO);
        Task RemoveActor(int id);
        Task UpdateActor(int id, ActorDTO actorDTO);
    }
}