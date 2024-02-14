using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.Entity;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IMovieRepository
    {
        void AddActor(int movieID, Actor actor);
        void AddActor(int movieID, int actorID);
        void AddMovie(Movie movie);
        void AddMovieCinema(int movieID, Cinema cinema);
        List<Movie> GetAllMovies();
        Movie GetMovie(int id);
        void RemoveActor(int movieID, Actor actor);
        void RemoveMovie(Movie movie);
        void RemoveMovieCinema(int movieID, Cinema cinema);
        Task SaveChangesAsync();
        void UpdateMovie(int id, Movie movie);
        void UpdateMovie(Movie movie);
    }
}