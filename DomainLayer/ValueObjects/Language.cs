using MoviesProject.DomainLayer.Interfaces;

namespace MoviesProject.DomainLayer.ValueObjects
{
    public class Language : IValueObject
    {
        public required string originalLanguage { get; set; }
        public ICollection<string>? spokenLanguages { get; set; }
    }
}
