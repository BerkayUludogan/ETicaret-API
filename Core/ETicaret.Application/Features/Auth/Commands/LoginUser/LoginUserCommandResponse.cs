using ETicaret.Application.Features.Auth.DTOs;

namespace ETicaret.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommandResponse
    {
        public required AuthUserDto User { get; set; }
        public required TokenDto Token { get; set; }
    }
}
