using AutoMapper;
using Microsoft.Identity.Client;
using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.Interfaces;
using MoviesProject.Infrastructure.Repos;

namespace MoviesProject.Application
{
    public class MovieQueryHandler : IMovieQueryHandler
    {
        public IMovieRepository _movierepository;
        public IMovieActorRepository _movieactorrepository;
        public IMapper _mapper;

        public MovieQueryHandler(IMovieRepository movierepository, IMapper mapper, IMovieActorRepository movieactorrepository)
        {
            _movierepository = movierepository;
            _mapper = mapper;
            _movieactorrepository = movieactorrepository;   
        }
        public async Task<List<MovieDTO>> GetAllMovies()
        {

            List<Movie> movies = await _movierepository.GetAllMovies();
            //List<MovieDTO> mappedMovies = movies
            //        .Select(movie => new MovieDTO
            //        {
            //            ID = movie.ID,
            //            Title = movie.Title,
            //            releaseDate = movie.releaseDate,
            //            Description = movie.Description,
            //            Length = movie.Length,
            //            PosterURL = movie.PosterURL,
            //            LanguageDTO = new LanguageDTO { originalLanguage=movie.Language.originalLanguage,
            //            spokenLanguages=movie.Language.spokenLanguages
            //            },
            //            GenreDTO = new GenreDTO{ 

            //                primaryGenre=movie.Genre.primaryGenre,
            //                subGenres=movie.Genre.subGenres
            //            }
            //        })
            //        .ToList();
            List<MovieDTO> mappedMovies = movies
                .Select(movie => _mapper.Map<MovieDTO>(movie)).ToList();
                    return mappedMovies;
        }
        
        public MovieDTO GetMovieByID(int id)
        {
            var movie = _movierepository.GetMovie(id);
            var movieDTO=_mapper.Map<MovieDTO>(movie);
            return movieDTO;
        }

       public List<ActorDTO> GetMovieActors(int movieID)
        {
            var actorDTO=_movieactorrepository.GetMovieActorsByMovieID(movieID);
            return actorDTO;
        }
    }
}
