using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoviesProject.Config;
using MoviesProject.DomainLayer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        JwtConfig _jwtConfig;
        public AuthController(SignInManager<ApplicationUser> _signInManager,
            UserManager<ApplicationUser> _userManager, IHttpContextAccessor _httpContextAccessor, 
            IOptionsMonitor<JwtConfig> _optionsMonitor, RoleManager<IdentityRole> roolemanager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            httpContextAccessor = _httpContextAccessor;
            _jwtConfig = _optionsMonitor.CurrentValue;
            roleManager = roolemanager;
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
            var emailExist= await userManager.FindByEmailAsync(form.Email);
            if(emailExist != null)
            {
                return BadRequest("email already exists");
            }
            var user = new ApplicationUser()
            {
                UserName = form.Username,
                Email=form.Email,
            };
            var createUserResult= await userManager.CreateAsync(user, form.Password);
            if(createUserResult.Succeeded)
            {
                string returnedToken = GenerateJwtToken(user, null);
                return Ok(new RegistrationLoginResponse()
                {
                    isSuccess=true,
                    Token=returnedToken,
                });
            }
            else
            {
                return BadRequest();
            }

            

        }

        // POST api/<AuthController>
        [HttpPost("auth/login")]
        public async Task<IActionResult> Login(LoginForm form)
        {
            DateTime refreshTokenExpiration;
            string refreshTokenString;
            var emailExists =await  userManager.FindByEmailAsync(form.Email);
            if (emailExists==null)
            {
                return BadRequest("email or password is wrong");
            }
            var passwordCorrect=await userManager.CheckPasswordAsync(emailExists,form.Password);
            if (!passwordCorrect)
            {
                return BadRequest("email or password is wrong");
            }
            var userRoles = await userManager.GetRolesAsync(emailExists);
            var token = GenerateJwtToken(emailExists, userRoles);
            if(emailExists.RefreshTokens.Any(t=>t.isActive)) {
                var activeRefreshToken = emailExists.RefreshTokens.FirstOrDefault(t => t.isActive);
                refreshTokenString = activeRefreshToken.token;
                refreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                refreshTokenString = refreshToken.token;
                refreshTokenExpiration = refreshToken.ExpiresOn;
                emailExists.RefreshTokens.Add(refreshToken);
                await userManager.UpdateAsync(emailExists);
            }
            if (!string.IsNullOrEmpty(refreshTokenString))
            {
                SetRefreshTokenInCookie(refreshTokenString, refreshTokenExpiration);
            }
            return Ok(new RegistrationLoginResponse() { isSuccess = true, 
                Token = token,
                refreshTokenExpiration=refreshTokenExpiration,
                refreshToken=refreshTokenString });
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var token=Request.Cookies["refreshToken"];

            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t=>t.token==token));

            if (user == null)
            {
                return BadRequest(new RegistrationLoginResponse()
                {
                    isSuccess = false,
                });
            }
            var refreshToken=user.RefreshTokens.SingleOrDefault(t => t.token==token);
            if (!refreshToken.isActive)
            {
                return BadRequest(new RegistrationLoginResponse()
                {
                    isSuccess = false,
                });
            }
            var roles = await userManager.GetRolesAsync(user);
            var jwtToken = GenerateJwtToken(user, roles);
            refreshToken.RevokedOn = DateTime.UtcNow;
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await userManager.UpdateAsync(user);
            SetRefreshTokenInCookie(newRefreshToken.token, newRefreshToken.ExpiresOn);
            return Ok(new RegistrationLoginResponse()
            {
                isSuccess=true,
                Token=jwtToken,
                refreshToken=newRefreshToken.token,
                refreshTokenExpiration=newRefreshToken.ExpiresOn,
            });
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
        [HttpPost("auth/createrole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {

                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            
            return Ok();

        }
        [HttpPost("auth/assignusertorole")]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors); 
            }

            return Ok("Role assigned successfully.");
        }
        [NonAction]
        public string GenerateJwtToken(ApplicationUser user, IList<string>? userRoles)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var claims = new List<Claim>()
              {
                new Claim("id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
              };
            if (userRoles != null && userRoles.Any())
            {
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                ,SecurityAlgorithms.HmacSha256)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken=jwtTokenHandler.WriteToken(token);
            return jwtToken;

    }
        [NonAction]
        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new Byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            var refreshToken = new RefreshToken()
            {
                token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow,

            };
            return refreshToken;
        }
        [NonAction]
        public void SetRefreshTokenInCookie(string refreshToken, DateTime expiresOn)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = expiresOn.ToLocalTime(),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }


    public class LoginForm
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegisterForm
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class RegistrationLoginResponse
    {
        public bool isSuccess { get; set; }
        public string Token { get; set; }
        [JsonIgnore]
        public string refreshToken { get; set; }
        public DateTime refreshTokenExpiration { get; set; }
    }

}
