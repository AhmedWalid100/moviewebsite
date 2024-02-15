using MoviesProject.Application;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IMovieCommandHandler
    {
        Task AddCinema(int movieID, CinemaDTO cinemaDTO);
        Task AddMovieActor(int movieID, int actorID);
        Task CreateMovieAsync(MovieDTO movieDTO);
        Task DeleteMovieAsync(int id);
        Task RemoveMovieActor(int movieID, int actorID);
        Task UpdateMovieDetails(int id, MovieDTO movieDTO);
    }
}