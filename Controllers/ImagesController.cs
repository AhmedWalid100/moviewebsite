using Microsoft.AspNetCore.Mvc;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _environment;
        public ImagesController(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
        {
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
        }

        // GET: api/<ImagesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ImagesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ImagesController>
        [HttpPost]
        public async Task<IActionResult> UploadMovieImageAsync(IFormFile formFile)
        {
            var uniqueID = Guid.NewGuid();
            //string filePath = this._environment.WebRootPath + "\\uploads\\images\\" + uniqueID;
            //if(!System.IO.Directory.Exists(filePath))
            //{
            //    System.IO.Directory.CreateDirectory(filePath);
            //}
            //string imagePath = filePath + "\\image" + ".jpg";
            string imagePath = "D:\\movies-frontend\\moviewebsite-frontend\\src\\assets\\movie-images\\" + uniqueID + ".jpg";
            //if (System.IO.File.Exists(imagePath))
            //{
            //    System.IO.File.Delete(imagePath);
            //}
            using (FileStream stream = System.IO.File.Create(imagePath))
            {
                await formFile.CopyToAsync(stream);
            }
            string sentImagePath = "./assets/movie-images/"+uniqueID;
            return Ok(sentImagePath);
        }

        // PUT api/<ImagesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ImagesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
