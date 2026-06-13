using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Categories.Rules;
using ETicaret.Domain.Entities.Catalog;
using MediatR;

namespace ETicaret.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, UpdateCategoryCommandResponse>
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly ICategoryBusinessRules _categoryBusinessRules;

        public UpdateCategoryCommandHandler(IUnitOfWork unitofWork, ICategoryBusinessRules categoryBusinessRules)
        {
            _unitofWork = unitofWork;
            _categoryBusinessRules = categoryBusinessRules;
        }

        public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryBusinessRules.CategoryMustExist(request.Id);

            await _categoryBusinessRules.CategoryNameMustBeUniqueForUpdate(category.Id, request.Name);
            await _categoryBusinessRules.CategorySlugMustBeUniqueForUpdate(category.Id, request.Slug);
            await _categoryBusinessRules.ParentCategoryMustExistIfProvided(category.ParentCategoryId);
            await _categoryBusinessRules.CategoryMustNotBeParentOfItself(request.Id, request.ParentCategoryId);

            category.Name = request.Name;
            category.Slug = request.Slug;
            category.Description = request.Description;
            category.IsActive = request.IsActive;
            category.ParentCategoryId = request.ParentCategoryId;
            category.ModifiedDate = DateTime.UtcNow;

            _unitofWork.GetWriteRepository<CategoryEntity>().Update(category);
            await _unitofWork.SaveAsync();

            return new UpdateCategoryCommandResponse { Id = category.Id };

        }
    }
}
