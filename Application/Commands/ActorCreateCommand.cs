namespace MoviesProject.Application.Commands
{
    public class ActorCreateCommand
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string? Bio { get; set; }

        public string? PosterURL { get; set; }
    }
}
