using FluentValidation;
using MediatR;

namespace ETicaret.Application.Common.Behaviors
{
    public class ValidationBehaviour<TReq, TRes> : IPipelineBehavior<TReq, TRes>
        where TReq : IRequest<TRes>
    {
        private readonly IEnumerable<IValidator<TReq>> validators;

        public ValidationBehaviour(IEnumerable<IValidator<TReq>> validators)
        {
            this.validators = validators;
        }

        public async Task<TRes> Handle(
            TReq request,
            RequestHandlerDelegate<TRes> next,
            CancellationToken cancellationToken)
        {
            if (!validators.Any())
                return await next(cancellationToken);

            var context = new ValidationContext<TReq>(request);

            var errors = validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .Select(f => f.ErrorMessage)
                .Distinct()
                .ToList();

            if (errors.Any())
                throw new Exceptions.ValidationException(errors);

            return await next(cancellationToken);
        }
    }
}