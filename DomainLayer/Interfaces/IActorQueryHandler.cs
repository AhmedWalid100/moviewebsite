using MoviesProject.Application;
using MoviesProject.Infrastructure.Repos;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IActorQueryHandler
    {
        ActorDTO GetActorById(int id);
        ReturnedCountAndData<ActorDTO> GetAllActors(int page, int pageSize,
            string? searchName, string orderColumn, string orderBy);
        List<MovieDTO> GetMoviesByActorID(int actorID);
    }
}