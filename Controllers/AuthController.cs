using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthController(SignInManager<IdentityUser> _signInManager,
            UserManager<IdentityUser> _userManager, IHttpContextAccessor _httpContextAccessor) {
            userManager = _userManager;
            signInManager = _signInManager;
            httpContextAccessor = _httpContextAccessor;
            
        }
        // GET: api/<AuthController>
        [HttpGet("auth/logout")]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
        [HttpGet("auth/user")]
        public async Task<IActionResult> UserInformation(ClaimsPrincipal? user)
        {
            user = httpContextAccessor.HttpContext?.User;  // Get user from context if not provided

            if (user == null)
            {
                return Unauthorized(); // Handle unauthenticated access
            }
            return Ok(user.Claims.ToDictionary(x => x.Type, x => x.Value)); 
        }
        [HttpPost("auth/register")]
        public async Task<IActionResult> Register(RegisterForm form)
        {
            if (form.Password != form.ConfirmPassword) {
                return BadRequest();
            }
            var user = new IdentityUser()
            {
                UserName = form.Username,

            };
            var createUserResult= await userManager.CreateAsync(user, form.Password);
            if(!createUserResult.Succeeded)
            {
                return BadRequest();
            }
            await signInManager.SignInAsync(user, true);
            return Ok();
            

        }

        // POST api/<AuthController>
        [HttpPost("auth/login")]
        public async Task<IActionResult> Login(LoginForm form)
        {
           var result= await signInManager.PasswordSignInAsync(form.Username,form.Password, true,false);
            if(result.Succeeded) {
                return Ok();
            }
            return BadRequest();
        }


        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
    public class LoginForm
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class RegisterForm
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
