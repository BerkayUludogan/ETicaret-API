namespace ETicaret.Application.Features.Auth.DTOs
{
    public class UserLoginAuditDto
    {
        public required string UserNameOrEmail { get; set; }
        public required string IPAddress { get; set; }
        public DateTime LoginTime { get; set; }
        public bool Success { get; set; } 
        public string? FailureReason { get; set; }
        public Guid? UserId { get; set; }
    }
}
