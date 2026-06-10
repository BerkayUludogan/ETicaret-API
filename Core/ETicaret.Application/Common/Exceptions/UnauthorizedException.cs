namespace ETicaret.Application.Shared.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string messageKey)
            : base(messageKey, 401)
        {
        }
    }
}
