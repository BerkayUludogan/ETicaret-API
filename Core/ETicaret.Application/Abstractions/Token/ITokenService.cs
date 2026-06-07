using ETicaret.Domain.Entities.Identity;

namespace ETicaret.Application.Abstractions.Token
{
    public interface ITokenService
    {
        Task<DTOs.Token> CreateAccessToken(AppUser user, IList<string> roles);
        string CreateRefreshToken();
    }
}
