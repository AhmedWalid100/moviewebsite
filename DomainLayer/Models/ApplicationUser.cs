using Microsoft.AspNetCore.Identity;

namespace MoviesProject.DomainLayer.Models
{
    public class ApplicationUser :IdentityUser
    {
        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
