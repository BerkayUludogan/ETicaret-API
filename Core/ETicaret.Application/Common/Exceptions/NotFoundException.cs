namespace ETicaret.Application.Common.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string messageKey)
            : base(messageKey, 404)
        {
        }
    }
}
