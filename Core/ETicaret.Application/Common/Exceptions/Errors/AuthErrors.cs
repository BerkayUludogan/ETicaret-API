namespace ETicaret.Application.Common.Exceptions.Errors
{
    public class AuthErrors
    {
        public const string InvalidCredentials   = "Auth.InvalidCredentials";
        public const string Unauthorized         = "Auth.Unauthorized";
        public const string UserNotActive        = "Auth.UserNotActive";
        public const string EmailNotConfirmed    = "Auth.EmailNotConfirmed";

        public const string RefreshTokenNotSaved = "Auth.RefreshTokenNotSaved";
        public const string InvalidRefreshToken  = "Auth.InvalidRefreshToken";
        public const string ExpiredRefreshToken  = "Auth.ExpiredRefreshToken";
        public const string UserLockedOut        = "Auth.UserLockedOut";
    }
}
