using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.Infrastructure.Repos;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IActorRepository
    {
        void AddActor(Actor actor);
        Actor GetActor(int id);
        void RemoveActor(Actor actor);
        Task SaveChangesAsync();
        void UpdateActor(int id, Actor actor);

        ReturnedCountAndData<Actor> GetActors(int page, int pageSize,
            string? searchName, string orderColumn, string orderBy);
    }
}