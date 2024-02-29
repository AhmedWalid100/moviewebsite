using MoviesProject.Application;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IMovieActorRepository
    {
        List<ActorDTO> GetMovieActorsByMovieID(int movieID);
        List<MovieDTO> GetMoviesByActorID(int actorID);

        void RemoveMovieActor(int movieID, int actorID);
    }
}