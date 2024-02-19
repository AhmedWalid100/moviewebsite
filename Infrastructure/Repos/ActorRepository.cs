using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.Interfaces;
using MoviesProject.Infrastructure.DBContext;

namespace MoviesProject.Infrastructure.Repos
{
    public class ActorRepository : IActorRepository
    {
        public MovieDBContext _context;
        public ActorRepository(MovieDBContext context)
        {
            _context = context;
        }
        public void AddActor(Actor actor)
        {
            _context.Actors.Add(actor);
        }

        public void RemoveActor(Actor actor)
        {
            _context.Actors.Remove(actor);
        }
        public List<Actor> GetActors()
        {
            return _context.Actors.ToList();
        }

        public Actor GetActor(int id)
        {
            return _context.Actors.Where(x => x.ID == id).FirstOrDefault();
        }
        public void UpdateActor(int id, Actor actor)
        {
            Actor oldActor = _context.Actors.FirstOrDefault(x => x.ID == id);
            if (oldActor != null)
            {
                foreach (var property in typeof(Actor).GetProperties())
                {
                    if (property.Name != "ID")
                    {
                        property.SetValue(oldActor, property.GetValue(actor));
                    }
                }
            }
            else
            {
                Console.WriteLine("Not Found");
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
