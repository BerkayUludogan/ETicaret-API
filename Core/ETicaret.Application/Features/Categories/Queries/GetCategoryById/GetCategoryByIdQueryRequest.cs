using ETicaret.Application.Features.Categories.DTOs;
using MediatR;

namespace ETicaret.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryRequest : IRequest<CategoryListDto>
    {
        public Guid Id { get; set; }
    }
}
