using ETicaret.Application.Consts;
using ETicaret.Application.DTOs.User;
using MediatR;

namespace ETicaret.Application.Features.Commands.Auth
{
    public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
    {
        public required CreateUserDto User{ get; set; }
        public string InvalidateCacheKeyPrefix => CacheKeys.AllUsers.Key;
    }
}
