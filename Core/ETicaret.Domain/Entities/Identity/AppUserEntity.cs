
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Domain.Entities.Identity
{
    public class AppUserEntity : IdentityUser<Guid>
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public virtual DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public virtual DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
