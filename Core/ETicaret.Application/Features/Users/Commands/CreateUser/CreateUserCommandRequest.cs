using ETicaret.Application.Common.Abstractions.Caching;
using ETicaret.Application.Common.Constants;
using MediatR;

namespace ETicaret.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>,IInvalidateCache
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
        public string InvalidateCacheKeyPrefix => CacheKeys.AllUsers.Key;
    }
}
