using ETicaret.Application.Features.Users.DTOs;

namespace ETicaret.Application.Shared.Abstractions.Services
{
    public interface IUserService
    {
        public Task<Guid> CreateAsync(UserResponseDto userDto);
    }
}
