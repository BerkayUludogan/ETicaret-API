namespace ETicaret.Application.DTOs.User.Base
{
    public class BaseUserDto
    {
        public Guid? Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string RoleName { get; set; }
        public string? Note { get; set; }
        public bool? IsAdmin { get; set; } = false;
        public bool IsActive { get; set; } 
    }
}
