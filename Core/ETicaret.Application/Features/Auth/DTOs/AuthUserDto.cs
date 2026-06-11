namespace ETicaret.Application.Features.Auth.DTOs
{
    public class AuthUserDto
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; } 
        public required string Email { get; set; }
        public IReadOnlyList<string> Roles { get; set; } = [];
    }
}
