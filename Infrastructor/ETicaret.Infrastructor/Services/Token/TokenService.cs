using ETicaret.Application.Abstractions.Token;
using ETicaret.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ETicaret.Infrastructor.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings _tokenSettings;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(UserManager<AppUser> userManager, TokenSettings tokenSettings)
        {
            _userManager = userManager;
            _tokenSettings = tokenSettings;
        }

        public async Task<Application.DTOs.Token> CreateAccessToken(AppUser user, IList<string> roles)
        {
            Application.DTOs.Token token = new();

            //Security key'in simetriği alınıyor.
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_tokenSettings.SecurityKey));
            //Şifrelenmiş kimlik oluşturma.
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            //Oluşturulacak token ayarları veriliyor.
            token.Expiration = DateTime.UtcNow.AddMinutes(_tokenSettings.TokenValidityInMinutes);
            token.RefreshTokenExpiration = DateTime.UtcNow.AddDays
                (_tokenSettings.RefreshTokenValidityInDays);

            var claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new(ClaimTypes.Email , user.Email),
                new(ClaimTypes.NameIdentifier,user.Id.ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            JwtSecurityToken securityToken = new(
                audience: _tokenSettings.Audience,
                issuer: _tokenSettings.Issuer,
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
                );

            //Token oluşturucu sınıftan örnek alınıyor.
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            token.AccessToken = handler.WriteToken(securityToken);

            token.RefreshToken = CreateRefreshToken();

            //AspNetUserClaims tablosunda aktif bir claims yapısı varsa önce onu sil daha sonra tekrardan oluştur.
            var userClaims = await _userManager.GetClaimsAsync(user);
            if (userClaims != null) await _userManager.RemoveClaimsAsync(user, userClaims);
            await _userManager.AddClaimsAsync(user, claims);

            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(number);
            return Convert.ToBase64String(number);
        }


    }
}
