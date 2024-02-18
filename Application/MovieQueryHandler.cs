using Microsoft.Identity.Client;
using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.Interfaces;
using MoviesProject.Infrastructure.Repos;

namespace MoviesProject.Application
{
    public class MovieQueryHandler : IMovieQueryHandler
    {
        public IMovieRepository _movierepository;

        public MovieQueryHandler(IMovieRepository movierepository)
        {
            _movierepository = movierepository;
        }
        public async Task<List<MovieDTO>> GetAllMovies()
        {

            List<Movie> movies = await _movierepository.GetAllMovies();
            List<MovieDTO> mappedMovies = movies
                    .Select(movie => new MovieDTO
                    {
                        Title = movie.Title,
                        releaseDate = movie.releaseDate,
                        Description = movie.Description,
                        Length = movie.Length,
                        PosterURL = movie.PosterURL,
                        LanguageDTO = new LanguageDTO { originalLanguage=movie.Language.originalLanguage,
                        spokenLanguages=movie.Language.spokenLanguages
                        },
                        GenreDTO = new GenreDTO{ 
                        
                            primaryGenre=movie.Genre.primaryGenre,
                            subGenres=movie.Genre.subGenres
                        }
                    })
                    .ToList();
                    return mappedMovies;
        }

       
    }
}
