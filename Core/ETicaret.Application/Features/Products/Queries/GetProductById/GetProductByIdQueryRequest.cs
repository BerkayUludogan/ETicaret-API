using ETicaret.Application.Features.Products.DTOs;
using MediatR;

namespace ETicaret.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryRequest : IRequest<ProductListDto>
    {
        public Guid Id { get; set; }
    }
}
