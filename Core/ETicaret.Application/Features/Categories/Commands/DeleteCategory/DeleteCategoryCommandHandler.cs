using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Categories.Rules;
using ETicaret.Domain.Entities.Catalog;
using MediatR;

namespace ETicaret.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, DeleteCategoryCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryBusinessRules _categoryBusinessRules;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryBusinessRules categoryBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _categoryBusinessRules = categoryBusinessRules;
        }

        public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryBusinessRules.CategoryMustExist(request.Id);
            
            category.IsDeleted = true;
            category.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.GetWriteRepository<CategoryEntity>().Update(category);
            await _unitOfWork.SaveAsync();

            return new DeleteCategoryCommandResponse
            {
                Id = request.Id,
            };

        }
    }
}
