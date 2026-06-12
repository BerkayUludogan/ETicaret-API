using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Categories.Rules;
using ETicaret.Domain.Entities.Catalog;
using MediatR;

namespace ETicaret.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, CreateCategoryCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryBusinessRules _categoryBusinessRules;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryBusinessRules categoryBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _categoryBusinessRules = categoryBusinessRules;
        }

        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await _categoryBusinessRules.CategoryNameMustBeUnique(request.Name);
            await _categoryBusinessRules.CategorySlugMustBeUnique(request.Slug);
            await _categoryBusinessRules.ParentCategoryMustExistIfProvided(request.ParentCategoryId);

            var category = new CategoryEntity
            {
                Name = request.Name,
                Slug = request.Slug,
                Description = request.Description,
                ParentCategoryId = request.ParentCategoryId,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };
            await _unitOfWork.GetWriteRepository<CategoryEntity>().AddAsync(category);
            await _unitOfWork.SaveAsync();

            return new CreateCategoryCommandResponse
            {
                Id = category.Id
            };
        }
    }
}
