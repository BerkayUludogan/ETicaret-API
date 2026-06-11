using MediatR;

namespace ETicaret.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandRequest : IRequest<RefreshTokenCommandResponse>
    {
        public required string RefreshToken { get; set; }
    }
}
