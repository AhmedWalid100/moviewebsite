using Microsoft.AspNetCore.Mvc;
using MoviesProject.DomainLayer.Aggregates;
using MoviesProject.DomainLayer.ValueObjects;
using MoviesProject.Infrastructure.DBContext;

namespace MoviesProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public object Get()
        {
            using (var context = new MovieDBContext())
            {
                //var actor = new Actor("Amr", 35, "Ali is his name");
                //context.Actors.Add(actor);
                //context.SaveChanges();

                var actor = context.Actors.FirstOrDefault(a => a.Name == "Amr");


                Language language = new Language("Arabic", "English");
                Genre genre = new Genre("Action", "Drama");
                var movie = new Movie("elgezira", "2008", "this is elgezira film", "img.img",
                    language, genre, "102");

                movie.AddCinema("Greenplaza", "Smo7a");
                movie.AddExistingActor(actor);
                context.Movies.Add(movie);
                context.SaveChanges();
            }
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
