using ETicaret.Application.Features.Auth.DTOs;

namespace ETicaret.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandResponse
    {
        public required TokenDto Token { get; set; }
    }
}
