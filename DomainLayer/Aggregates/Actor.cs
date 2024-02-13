using MoviesProject.DomainLayer.Interfaces;

namespace MoviesProject.DomainLayer.Aggregates
{
    public class Actor:IAggregateRoot, IEntity
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }

        public string? Bio { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
