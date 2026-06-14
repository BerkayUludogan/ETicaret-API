using ETicaret.Application.Common.Validation;
using FluentValidation;

namespace ETicaret.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommandRequest>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).ProductId();
        }
    }
}
