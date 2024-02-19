using MoviesProject.Application;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IMovieQueryHandler
    {
        Task<List<MovieDTO>> GetAllMovies();
        MovieDTO GetMovieByID(int id);
        List<ActorDTO> GetMovieActors(int movieID);
    }
}