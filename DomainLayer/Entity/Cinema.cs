using MoviesProject.DomainLayer.Interfaces;

namespace MoviesProject.DomainLayer.Entity
{
    public class Cinema :IEntity
    {
        public int ID { get; set; }
        public required string Name { get; set; }

        public required string Address { get; set; }

    }
}
