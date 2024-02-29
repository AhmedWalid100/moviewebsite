using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.Entity;
using MoviesProject.Infrastructure.Repos;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IMovieRepository
    {
        void AddActor(int movieID, Actor actor);
        void AddActor(int movieID, int actorID);
        void AddMovie(Movie movie);
        void AddMovieCinema(int movieID, Cinema cinema);
        Task<ReturnedCountAndData<Movie>> GetAllMovies(int page, int pageSize,
                    string? searchTitle, string? searchGenre, string orderColumn, string orderBy);
        Movie GetMovie(int id);
        void RemoveActor(int movieID, int actorID);
        void RemoveMovie(Movie movie);
        void RemoveMovieCinema(int movieID, Cinema cinema);
        Task SaveChangesAsync();
        Task UpdateMovie(int id, Movie movie);
        //void UpdateMovie(Movie movie);
    }
}