using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.Entity;
using MoviesProject.DomainLayer.Interfaces;
using MoviesProject.Infrastructure.DBContext;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public Movie? GetMovie(int id)
        {
            return _context.Movies.Where(x=>x.ID==id).FirstOrDefault();
        }
        public List<Movie> GetAllMovies()
        {
            return _context.Movies.ToList();
        }
        //public void UpdateMovie(Movie movie)
        //{
        //    _context.Update<Movie>(movie);
        //    //_context.SaveChanges();
        //}
        public async Task UpdateMovie(int id, Movie movie) //more ideal
        {

            Movie oldMovie =  _context.Movies.FirstOrDefault(x=>x.ID==id);
            if (oldMovie != null)
            {
                foreach (var property in typeof(Movie).GetProperties())
                {
                    if (property.Name != "ID")
                    {
                        property.SetValue(oldMovie, property.GetValue(movie));
                    }
                }
            }
            else
            {
                Console.WriteLine("hey");
            }
            


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
        public void RemoveActor(int movieID, int actorID)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.ID == movieID);
            movie.RemoveActor(actorID);
            //_context.SaveChanges();
        }


    }
}
