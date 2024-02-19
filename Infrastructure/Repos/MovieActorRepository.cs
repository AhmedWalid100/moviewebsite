using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MoviesProject.Application;
using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.Interfaces;
using MoviesProject.Infrastructure.DBContext;

namespace MoviesProject.Infrastructure.Repos
{
    public class MovieActorRepository : IMovieActorRepository
    {
        public MovieDBContext _dbcontext;
        public MovieActorRepository(MovieDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public List<ActorDTO> GetMovieActorsByMovieID(int movieID)
        {
            var actors = _dbcontext.MoviesActors.Where(x => x.MovieID == movieID).Include(x => x.Actor)
                .Select(movieActor => new ActorDTO()
                {
                    ID = movieActor.Actor.ID,
                    Name = movieActor.Actor.Name,
                    Age = movieActor.Actor.Age,
                    Bio = movieActor.Actor.Bio,
                    PosterURL= movieActor.Actor.PosterURL,
                }).ToList();
            return actors;
        }
        public List<MovieDTO> GetMoviesByActorID(int actorID)
        {
            var movies = _dbcontext.MoviesActors.Where(x => x.ActorID == actorID).Include(x => x.Movie)
                .Select(movieActor => new MovieDTO()
                {
                    ID = movieActor.Movie.ID,
                    Title = movieActor.Movie.Title,
                    releaseDate = movieActor.Movie.releaseDate,
                    Description = movieActor.Movie.Description,
                    Length = movieActor.Movie.Length,
                    PosterURL = movieActor.Movie.PosterURL,
                    LanguageDTO=new LanguageDTO()
                    {
                        originalLanguage=movieActor.Movie.Language.originalLanguage,
                        spokenLanguages=movieActor.Movie.Language.spokenLanguages
                    },
                    GenreDTO=new GenreDTO()
                    {
                        primaryGenre=movieActor.Movie.Genre.primaryGenre,
                        subGenres=movieActor.Movie.Genre.subGenres,
                    }
                }).ToList();
            return movies;
        }
    }
}
