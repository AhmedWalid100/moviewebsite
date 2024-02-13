namespace MoviesProject.DomainLayer.Aggregates
{
    public class MovieActor
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public int ActorID { get; set; }
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
    }
}
