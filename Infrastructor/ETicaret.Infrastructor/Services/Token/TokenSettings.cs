namespace ETicaret.Infrastructor.Services.Token
{
    public class TokenSettings
    {
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string SecurityKey { get; set; } = string.Empty;
        public int TokenValidityInMinutes { get; set; }
        public int RefreshTokenValidityInDays { get; set; }
    }
}
