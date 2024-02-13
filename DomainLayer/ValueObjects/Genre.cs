using MoviesProject.DomainLayer.Interfaces;

namespace MoviesProject.DomainLayer.ValueObjects
{
    public class Genre: IValueObject
    {

        public required string primaryGenre { get; set; }
        public ICollection<string>? subGenres { get; set; }
    }
}
