namespace ETicaret.Application.Shared.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(string messageKey)
            : base(new List<string> { messageKey }, 400) { }

        public ValidationException(List<string> messageKeys)
            : base(messageKeys, 400) { }
    }
}
