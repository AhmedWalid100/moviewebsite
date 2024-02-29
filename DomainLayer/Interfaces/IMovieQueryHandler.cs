using MoviesProject.Application;
using MoviesProject.Infrastructure.Repos;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IMovieQueryHandler
    {
        //Task<List<MovieDTO>> GetAllMovies();
        MovieDTO GetMovieByID(int id);
        List<ActorDTO> GetMovieActors(int movieID);
        Task<ReturnedCountAndData<MovieDTO>> GetAllMoviesAfterOperations(int page, int pageSize,
            string? searchTitle, string? searchGenre, string orderColumn, string orderBy);
    }
}