using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.Entity;
using MoviesProject.DomainLayer.Interfaces;

namespace MoviesProject.Application
{
    public class MovieCommandHandler
    {
        public IMovieRepository _movierepository;
        public MovieCommandHandler(IMovieRepository movierepository)
        {
            _movierepository = movierepository;
        }
        public async Task CreateMovieAsync(MovieDTO movieDTO)
        {
            var movie = new Movie(movieDTO.Title, movieDTO.releaseDate, movieDTO.Description, 
                movieDTO.PosterURL,movieDTO.Language, movieDTO.Genre, movieDTO.Length);
            _movierepository.AddMovie(movie);
            await _movierepository.SaveChangesAsync();

        }
        public async Task DeleteMovieAsync(int id)
        {
            var movie=_movierepository.GetMovie(id);
            _movierepository.RemoveMovie(movie);
            await _movierepository.SaveChangesAsync();
        }
        public async Task UpdateMovieDetails(int id, MovieDTO movieDTO)
        {
            var movie = new Movie(movieDTO.Title, movieDTO.releaseDate, movieDTO.Description,
                movieDTO.PosterURL, movieDTO.Language, movieDTO.Genre, movieDTO.Length);
            _movierepository.UpdateMovie(id, movie);
            await _movierepository.SaveChangesAsync();
        }
        public async Task AddMovieActor(int movieID, int actorID)
        {
            _movierepository.AddActor(movieID, actorID);
            await _movierepository.SaveChangesAsync(); 
        }
        public async Task AddCinema(int movieID,CinemaDTO cinemaDTO)
        {
            var movie=_movierepository.GetMovie(movieID);
            movie.AddCinema(cinemaDTO.Name, cinemaDTO.Address);
            await _movierepository.SaveChangesAsync();
        }
        
    }
}
