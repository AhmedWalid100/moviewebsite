using MoviesProject.DomainLayer.ValueObjects;

namespace MoviesProject.Application
{
    public class LanguageDTO
    {
        public string? originalLanguage { get; set; }
        public string? spokenLanguages { get; set; }

        public static explicit operator LanguageDTO(Language v)
        {
            throw new NotImplementedException();
        }
    }
}
