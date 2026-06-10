namespace ETicaret.Application.Shared.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string messageKey)
            : base(messageKey, 404)
        {
        }
    }
}
