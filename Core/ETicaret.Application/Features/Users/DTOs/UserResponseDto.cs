namespace ETicaret.Application.Features.Users.DTOs
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; } 
        public required string Email { get; set; }
    }
}
