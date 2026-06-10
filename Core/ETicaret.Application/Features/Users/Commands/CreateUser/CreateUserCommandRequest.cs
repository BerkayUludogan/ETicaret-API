using ETicaret.Application.Shared.Consts;
using MediatR;

namespace ETicaret.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } 
        public string Password { get; set; } 

        public string InvalidateCacheKeyPrefix => CacheKeys.AllUsers.Key;
    }
}
