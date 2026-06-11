using MediatR;
using System.ComponentModel;

namespace ETicaret.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        [DefaultValue("admin@gmail.com")]
        public required string Email { get; set; }
        [DefaultValue("123456")]
        public required string Password { get; set; }
    }
}
