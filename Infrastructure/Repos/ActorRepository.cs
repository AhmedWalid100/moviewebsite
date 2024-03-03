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
        public ReturnedCountAndData<Actor> GetActors(int page, int pageSize,
            string? searchName, string orderColumn, string orderBy)
        {
            var actors= _context.Actors.AsQueryable();
            if(searchName is not null)
            {
                actors = actors.Where(a => a.Name.ToLower().Contains(searchName.ToLower()));
            }
            var count = actors.Count();
            if (orderBy.ToLower() == "asc")
            {
                if (orderColumn.ToLower() == "id")
                {
                    actors = actors.OrderBy(m => m.ID);
                }
                if (orderColumn.ToLower() == "name")
                {
                    actors = actors.OrderBy(m => m.Name);
                }
            }
            if (orderBy.ToLower() == "desc")
            {
                if (orderColumn.ToLower() == "id")
                {
                    actors = actors.OrderByDescending(m => m.ID);
                }
                if (orderColumn.ToLower() == "name")
                {
                    actors = actors.OrderByDescending(m => m.Name);
                }
            }
            var returnedactors= actors.Skip((page-1)*pageSize).Take(pageSize).ToList();
            var returnedData = new ReturnedCountAndData<Actor>()
            {
                count = count,
                data = returnedactors
            };
            return returnedData;
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
