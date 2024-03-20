using Microsoft.EntityFrameworkCore;

namespace MoviesProject.DomainLayer.Models
{
    [Owned]
    public class RefreshToken
    {
        public string token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; }
        public bool isActive => RevokedOn == null && IsExpired == false;
    }
}
