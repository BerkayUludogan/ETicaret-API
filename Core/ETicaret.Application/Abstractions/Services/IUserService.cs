using ETicaret.Application.DTOs.User;

namespace ETicaret.Application.Abstractions.Services
{
    public interface IUserService
    {
        public Task<Guid> CreateAsync(CreateUserDto userDto);
    }
}
