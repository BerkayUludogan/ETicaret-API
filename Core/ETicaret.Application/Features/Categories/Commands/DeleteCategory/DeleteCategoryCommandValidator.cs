using ETicaret.Application.Common.Validation;
using FluentValidation;

namespace ETicaret.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommandRequest>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id).CategoryId();
        }
    }
}
