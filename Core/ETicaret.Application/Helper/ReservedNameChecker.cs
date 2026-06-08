using ETicaret.Application.Consts;

namespace ETicaret.Application.Helper
{
    public static class ReservedNameChecker
    {
        public static bool IsReservedRole(string RoleName)
         => ReservedNames.RoleNames
            .Any(r => string.Equals(r, RoleName, StringComparison.OrdinalIgnoreCase));

        public static bool IsReservedUserName(string UserName)
            => ReservedNames.UserNames
                .Any(r => string.Equals(r, UserName, StringComparison.OrdinalIgnoreCase));
    }
}
