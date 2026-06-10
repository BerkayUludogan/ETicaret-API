using ETicaret.Application.Features.Users.DTOs;

namespace ETicaret.Application.Common.Abstractions.Services
{
    public interface IUserService
    {
        public Task<Guid> CreateAsync(UserResponseDto userDto);
    }
}
