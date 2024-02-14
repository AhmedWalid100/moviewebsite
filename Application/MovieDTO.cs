using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.Entity;
using MoviesProject.DomainLayer.ValueObjects;

namespace MoviesProject.Application
{
    public class MovieDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string releaseDate { get; set; }
        public string Description { get; set; }
        public string? Length { get; set; }
        public string PosterURL { get; set; }
        public Language Language { get; set; }
        public Genre Genre { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }

        public ICollection<Cinema> Cinemas { get; set; }
    }
}
