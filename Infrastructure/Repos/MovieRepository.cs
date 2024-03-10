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
        public async Task<ReturnedCountAndData<Movie>> GetAllMovies(int page, int pageSize,
            string? searchTitle, string? searchGenre, string orderColumn, string orderBy)
        {


            var movies = _context.Movies.AsQueryable();

            if (searchTitle is not null)
            {
                movies = movies.Where(m => m.Title.ToLower().Contains(searchTitle.ToLower()));
            }
            if (searchGenre is not null)
            {
                movies = movies.Where(m => m.Genre.primaryGenre.ToLower().Equals(searchGenre.ToLower()));
            }
            var count = movies.Count();
            if (orderBy.ToLower() == "asc")
            {
                if (orderColumn.ToLower() == "id")
                {
                    movies = movies.OrderBy(m => m.ID);
                }
                if (orderColumn.ToLower() == "title")
                {
                    movies = movies.OrderBy(m => m.Title);
                }
                if (orderColumn.ToLower() == "releasedate")
                {
                    movies = movies.OrderBy(m => m.releaseDate);
                }
            }
            if (orderBy.ToLower() == "desc")
            {
                if (orderColumn.ToLower() == "id")
                {
                    movies = movies.OrderByDescending(m => m.ID);
                }
                if (orderColumn.ToLower() == "title")
                {
                    movies = movies.OrderByDescending(m => m.Title);
                }
                if (orderColumn.ToLower() == "releasedate")
                {
                    movies = movies.OrderByDescending(m => m.releaseDate);
                }
            }

            var returnedMovies = movies.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ReturnedCountAndData<Movie> returnedData = new ReturnedCountAndData<Movie>{ count=count,data=returnedMovies};


            return returnedData;
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
                throw new Exception("Movie does not exist.");
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
