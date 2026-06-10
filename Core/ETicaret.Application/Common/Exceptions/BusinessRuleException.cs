namespace ETicaret.Application.Shared.Exceptions
{
    public class BusinessRuleException : BaseException
    {
        public BusinessRuleException(string messageKey)
            : base(messageKey, 422){}
    }
}
