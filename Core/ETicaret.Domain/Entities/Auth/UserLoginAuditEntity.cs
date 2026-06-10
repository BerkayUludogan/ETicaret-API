using ETicaret.Domain.Entities.Common;

namespace ETicaret.Domain.Entities.Auth
{
    public class UserLoginAuditEntity : BaseEntity
    {
        public required string UserNameOrEmail { get; set; }
        public required string IPAddress { get; set; }
        public DateTime LoginTime { get; set; }
        public bool Success { get; set; } 
        public string? FailureReason { get; set; }
        public Guid? UserId { get; set; }
    }
}
