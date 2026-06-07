using Microsoft.AspNetCore.Identity;

namespace ETicaret.Domain.Entities.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }
    }
}
