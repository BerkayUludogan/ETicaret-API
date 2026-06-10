using ETicaret.Domain.Entities.Identity;

namespace ETicaret.Application.Shared.Abstractions.Token
{
    public interface ITokenService
    {
        Task<DTOs.Token> CreateAccessToken(AppUserEntity user, IList<string> roles);
        string CreateRefreshToken();
    }
}
