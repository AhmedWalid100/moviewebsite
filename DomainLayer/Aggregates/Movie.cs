using MoviesProject.DomainLayer.Entity;
using MoviesProject.DomainLayer.Interfaces;
using MoviesProject.DomainLayer.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace MoviesProject.DomainLayer.Aggregates
{
    public class Movie : IAggregateRoot, IEntity
    {
        public int ID { get; set; }
        public string Title { get; private set; }
        public string releaseDate { get; private set; }
        public string Description { get; private set; }
        public string? Length { get; private set; }
        public string PosterURL { get; private set; }
        public Language Language { get; private set; }
        public Genre Genre {  get; private set; }
        public ICollection<MovieActor> MovieActors { get; private set; }

        public ICollection<Cinema> Cinemas { get; private set; }
        public Movie()
        {
            MovieActors = [];
            Cinemas = [];
        }
        [SetsRequiredMembers]
        public Movie(string _title, string _releasedate, string _description,
            string _posterURL, Language _langugage, Genre _genre,
             string? _length = "", ICollection<MovieActor>? _movieactors = null,
            ICollection<Cinema>? _cinemas = null)
        {
            Title = _title;
            releaseDate = _releasedate;
            Description = _description;
            Length = _length;
            PosterURL = _posterURL;
            Language = _langugage;
            Genre = _genre;
            if (_cinemas == null)
            {
                Cinemas = [];
            }
            else
            {
                Cinemas = _cinemas;
            }
            if (_movieactors == null)
            {
                MovieActors = [];
            }
            else
            {
                MovieActors = _movieactors;
            }

        }

        public void AddCinema(string _name, string _address)
        {
            var cinema = new Cinema(_name, _address);
            Cinemas.Add(cinema);
        }
        public void AddNewActor(string _name, int _age, string? _bio="")
        {
            var actor = new Actor(_name, _age,_bio);
            var movieActor = new MovieActor()
            {
                Movie=this,
                Actor=actor
            };
            MovieActors.Add(movieActor);
        }
        
        public void AddExistingActor(Actor _actor)
        {
            var actor = _actor;
            var movieActor = new MovieActor()
            {
                Movie = this,
                Actor = actor
            };
            MovieActors.Add(movieActor);
        }
        public void AddExistingActor(int actorID)
        {
            
            var movieActor = new MovieActor()
            {
                MovieID = this.ID,
                ActorID= actorID
            };
            MovieActors.Add(movieActor);
        }
        public void RemoveActor(int actorID)
        {
            var movieActor = MovieActors.FirstOrDefault(x => x.ActorID == actorID && x.MovieID == this.ID);
            MovieActors.Remove(movieActor);
        }
        public void UpdateGenre(string _primarygenre, string _secondarygenres)
        {
            var genre = new Genre(_primarygenre, _secondarygenres);
            this.Genre = genre;
        }
        public void UpdateLanguage(string _originallanguage, string _spokenlanguages)
        {
            var language = new Language(_originallanguage, _spokenlanguages);
            this.Language = language;
        }
    }
}
