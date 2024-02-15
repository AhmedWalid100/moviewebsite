using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.Entity;
using MoviesProject.DomainLayer.ValueObjects;

namespace MoviesProject.Application
{
    public class MovieDTO
    {
        
        public string Title { get; set; }
        public string releaseDate { get; set; }
        public string Description { get; set; }
        public string? Length { get; set; }
        public string PosterURL { get; set; }
        public LanguageDTO LanguageDTO { get; set; }
        public GenreDTO GenreDTO { get; set; }

    }
}
