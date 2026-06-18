using System.Security.Claims;

namespace ETicaret.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                return null;

            return Guid.TryParse(userId, out var parsedUserId)
                ? parsedUserId
                : null;
        }
    }
}
