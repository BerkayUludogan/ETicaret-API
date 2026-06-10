using ETicaret.Application.Features.Auth.DTOs;
using ETicaret.Domain.Entities.Identity;

namespace ETicaret.Application.Common.Abstractions.Token
{
    public interface ITokenService
    {
        Task<TokenDto> CreateAccessToken(AppUserEntity user, IList<string> roles);
        string CreateRefreshToken();
    }
}
