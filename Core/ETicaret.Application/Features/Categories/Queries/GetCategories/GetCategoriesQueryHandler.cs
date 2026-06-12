using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Categories.DTOs;
using ETicaret.Domain.Entities.Catalog;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQueryRequest, List<CategoryListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CategoryListDto>> Handle(GetCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork
                .GetReadRepository<CategoryEntity>()
                .GetWhere(x => !x.IsDeleted && x.IsActive, false)
                .OrderBy(x => x.Name)
                .Select(x => new CategoryListDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Description = x.Description,
                    ParentCategoryId = x.ParentCategoryId,
                    IsActive = x.IsActive
                }).ToListAsync(cancellationToken);
        }
    }
}
