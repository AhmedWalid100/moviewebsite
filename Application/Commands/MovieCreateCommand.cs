using System.ComponentModel.DataAnnotations;

namespace MoviesProject.Application.Commands
{
    public class MovieCreateCommand
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string releaseDate { get; set; }

        public string Description { get; set; }
        public string? Length { get; set; }

        public string PosterURL { get; set; }
        public LanguageDTO? LanguageDTO { get; set; }

        public GenreDTO? GenreDTO { get; set; }
    }
}
