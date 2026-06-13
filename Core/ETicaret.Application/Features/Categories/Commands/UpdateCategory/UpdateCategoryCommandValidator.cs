using ETicaret.Application.Common.Constants.FieldLengths;
using ETicaret.Application.Common.Validation;
using FluentValidation;

namespace ETicaret.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommandRequest>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id).CategoryId();
            RuleFor(x => x.Name).CategoryName();
            RuleFor(x => x.Slug).CategorySlug();
            RuleFor(x => x.Description).CategoryDescription();
        }
    }
}
