using MediatR;

namespace ETicaret.Application.Features.Auth.Commands.Logout
{
    public class LogoutCommandRequest : IRequest
    {
        public required Guid UserId{ get; set; }
    }
}
