﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoviesProject.Config;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        JwtConfig _jwtConfig;
        public AuthController(SignInManager<IdentityUser> _signInManager,
            UserManager<IdentityUser> _userManager, IHttpContextAccessor _httpContextAccessor, 
            IOptionsMonitor<JwtConfig> _optionsMonitor) {
            userManager = _userManager;
            signInManager = _signInManager;
            httpContextAccessor = _httpContextAccessor;
            _jwtConfig = _optionsMonitor.CurrentValue;
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
            var user = new IdentityUser()
            {
                UserName = form.Username,
                Email=form.Email,
            };
            var createUserResult= await userManager.CreateAsync(user, form.Password);
            if(createUserResult.Succeeded)
            {
                string returnedToken = GenerateJwtToken(user);
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
            var token = GenerateJwtToken(emailExists);
            return Ok(new RegistrationLoginResponse() { isSuccess = true, Token = token });
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
        [NonAction]
        public string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }
                ),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                ,SecurityAlgorithms.HmacSha256)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken=jwtTokenHandler.WriteToken(token);
            return jwtToken;

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
    }

}
