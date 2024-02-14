using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.Entity;
using MoviesProject.DomainLayer.Interfaces;
using MoviesProject.Infrastructure.DBContext;
using System.Linq;

namespace MoviesProject.Infrastructure.Repos
{
    public class MovieRepository : IMovieRepository
    {
        public MovieDBContext _context;
        public MovieRepository(MovieDBContext context)
        {
            _context = context;
        }

        public void AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            //_context.SaveChanges();
        }
        public void RemoveMovie(Movie movie)
        {
            _context.Movies.Remove(movie);
        }
        public Movie GetMovie(int id)
        {
            return _context.Movies.Find(id);
        }
        public List<Movie> GetAllMovies()
        {
            return _context.Movies.ToList();
        }
        public void UpdateMovie(Movie movie)
        {
            _context.Update<Movie>(movie);
            //_context.SaveChanges();
        }
        public void UpdateMovie(int id, Movie movie) //more ideal
        {
            var oldMovie = GetMovie(id);
            _context.Entry(oldMovie).CurrentValues.SetValues(movie);
            //_context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void AddMovieCinema(int movieID, Cinema cinema)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.ID == movieID);
            movie.Cinemas.Add(cinema);
            //_context.SaveChanges();
        }
        public void RemoveMovieCinema(int movieID, Cinema cinema)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.ID == movieID);
            movie.Cinemas.Remove(cinema);
            //_context.SaveChanges();
        }
        public void AddActor(int movieID, Actor actor)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.ID == movieID);
            movie.AddExistingActor(actor);
            //_context.SaveChanges();
        }
        public void AddActor(int movieID, int actorID)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.ID == movieID);
            movie.AddExistingActor(actorID);
            //_context.SaveChanges();
        }
        public void RemoveActor(int movieID, Actor actor)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.ID == movieID);
            movie.RemoveActor(actor);
            //_context.SaveChanges();
        }


    }
}
