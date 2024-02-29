using MoviesProject.DomainLayer.Aggregates;

namespace MoviesProject.Infrastructure.Repos
{
    public class ReturnedCountAndData<T>
    {
        public int count { get; set; }
        public List<T> data { get; set; }
    }
}
