namespace ETicaret.Application.Common.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string messageKey)
            : base(messageKey, 401)
        {
        }
    }
}
