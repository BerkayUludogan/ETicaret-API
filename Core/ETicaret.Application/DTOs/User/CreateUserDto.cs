using ETicaret.Application.DTOs.User.Base;

namespace ETicaret.Application.DTOs.User
{
    public class CreateUserDto : BaseUserDto
    {
        public required string Password { get; set; }
        public required string PasswordConfirm { get; set; }
    }
}
