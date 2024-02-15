using MoviesProject.DomainLayer.Interfaces;

namespace MoviesProject.DomainLayer.ValueObjects
{
    public class Genre: IValueObject
    {

        public string primaryGenre { get; private set; }
        public string subGenres { get; private set; }
        public Genre()
        {

        }
        public Genre(string primaryGenre, string subGenres)
        {
            this.primaryGenre = primaryGenre;
            this.subGenres = subGenres;
        }
    }
}
