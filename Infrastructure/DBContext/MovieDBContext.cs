using Microsoft.EntityFrameworkCore;
using MoviesProject.DomainLayer.Aggregates;

namespace MoviesProject.Infrastructure.DBContext
{
    public class MovieDBContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MovieDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Movie>().ComplexProperty(m => m.Genre);
            modelbuilder.Entity<Movie>().ComplexProperty(m => m.Language);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieActor> MoviesActors { get; set; }
    }
}
