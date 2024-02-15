using MoviesProject.DomainLayer.Interfaces;

namespace MoviesProject.DomainLayer.ValueObjects
{
    public class Language : IValueObject
    {
        public string originalLanguage { get; private set; }
        public string spokenLanguages { get; private set; }
        public Language()
        {

        }
        public Language(string originalLanguage, string spokenLanguages)
        {
            this.originalLanguage = originalLanguage;
            this.spokenLanguages = spokenLanguages;
        }
    }
}
