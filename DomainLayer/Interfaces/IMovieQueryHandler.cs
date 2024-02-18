using MoviesProject.Application;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IMovieQueryHandler
    {
        Task<List<MovieDTO>> GetAllMovies();
    }
}