using ETicaret.Application.Features.Categories.DTOs;
using ETicaret.Application.Features.Categories.Rules;
using MediatR;

namespace ETicaret.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQueryRequest, CategoryListDto>
    {
        private readonly ICategoryBusinessRules _categoryBusinessRules;
        public GetCategoryByIdQueryHandler(ICategoryBusinessRules categoryBusinessRules)
        {
            _categoryBusinessRules = categoryBusinessRules;
        }

        public async Task<CategoryListDto> Handle(GetCategoryByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryBusinessRules.CategoryMustExist(request.Id);
            return new CategoryListDto
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                Description = category.Description,
                ParentCategoryId = category.ParentCategoryId,
                IsActive = category.IsActive
            };
        }
    }
}
