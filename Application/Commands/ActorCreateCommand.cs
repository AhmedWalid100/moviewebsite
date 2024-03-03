using System.ComponentModel.DataAnnotations;

namespace MoviesProject.Application.Commands
{
    public class ActorCreateCommand
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        public string? Bio { get; set; }

        public string? PosterURL { get; set; }
    }
}
