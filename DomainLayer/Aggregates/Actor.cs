using MoviesProject.DomainLayer.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace MoviesProject.DomainLayer.Aggregates
{
    public class Actor:IAggregateRoot, IEntity
    {
        public int ID { get; set; }
        public string Name { get; private set; }
        public int Age { get; private set; }

        public string? Bio { get; private set; }

        public string? PosterURL { get; private set; }
        public ICollection<MovieActor>? MovieActors { get; set; }

        public Actor()
        {

        }

        [SetsRequiredMembers]
        public Actor(string _name, int _age, string _bio, string posterURL)
        {
            Name = _name;
            Age = _age;
            Bio = _bio;
            PosterURL = posterURL;
        }

        public static explicit operator List<object>(Actor v)
        {
            throw new NotImplementedException();
        }
    }
}
