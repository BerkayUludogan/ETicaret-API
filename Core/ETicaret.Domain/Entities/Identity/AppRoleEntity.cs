using Microsoft.AspNetCore.Identity;

namespace ETicaret.Domain.Entities.Identity
{
    public class AppRoleEntity : IdentityRole<Guid>
    {
        public string? Description { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
