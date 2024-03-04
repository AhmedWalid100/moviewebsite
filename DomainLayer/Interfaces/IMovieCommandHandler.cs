using MoviesProject.Application;
using MoviesProject.Application.Commands;
using MoviesProject.Application.ResponseModels;

namespace MoviesProject.DomainLayer.Interfaces
{
    public interface IMovieCommandHandler
    {
        Task AddCinema(int movieID, CinemaDTO cinemaDTO);
        Task AddMovieActor(int movieID, int actorID);
        Task<CreateResponse<MovieDTO>> CreateMovieAsync(MovieCreateCommand movieCommand);
        Task DeleteMovieAsync(int id);
        Task RemoveMovieActor(int movieID, int actorID);
        Task UpdateMovieDetails(int id, MovieCreateCommand movieCommand);
    }
}