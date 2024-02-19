using MoviesProject.Application;
using MoviesProject.Application.Commands;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IMovieCommandHandler
    {
        Task AddCinema(int movieID, CinemaDTO cinemaDTO);
        Task AddMovieActor(int movieID, int actorID);
        Task CreateMovieAsync(MovieCreateCommand movieCommand);
        Task DeleteMovieAsync(int id);
        Task RemoveMovieActor(int movieID, int actorID);
        Task UpdateMovieDetails(int id, MovieCreateCommand movieCommand);
    }
}