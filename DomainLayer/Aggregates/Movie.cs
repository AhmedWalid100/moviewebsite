using MoviesProject.DomainLayer.Entity;
using MoviesProject.DomainLayer.Interfaces;
using MoviesProject.DomainLayer.ValueObjects;

namespace MoviesProject.DomainLayer.Aggregates
{
    public class Movie : IAggregateRoot, IEntity
    {
        public int ID { get; set; }
        public required string Title { get; set; }
        public required string releaseDate { get; set; }
        public required string Description { get; set; }
        public string? Length { get; set; }
        public required string PosterURL { get; set; }
        public required Language Language { get; set; }
        public required Genre Genre {  get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }

        public ICollection<Cinema> Cinemas { get; set; }

    }
}
