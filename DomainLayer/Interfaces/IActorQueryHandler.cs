using MoviesProject.Application;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IActorQueryHandler
    {
        ActorDTO GetActorById(int id);
        List<ActorDTO> GetAllActors();
        List<MovieDTO> GetMoviesByActorID(int actorID);
    }
}