using MoviesProject.DomainLayer.Aggregates;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IActorRepository
    {
        void AddActor(Actor actor);
        Actor GetActor(int id);
        void RemoveActor(Actor actor);
        Task SaveChangesAsync();
        void UpdateActor(int id, Actor actor);
    }
}