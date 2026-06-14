using ETicaret.Application.Common.Abstractions.AutoMapper;
using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Products.Rules;
using ETicaret.Domain.Entities.Catalog;
using MediatR;

namespace ETicaret.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductBusinessRules _productBusinessRules;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IProductBusinessRules productBusinessRules, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _productBusinessRules = productBusinessRules;
            _mapper = mapper;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.ProductCategoryMustExist(request.CategoryId);
            await _productBusinessRules.ProductSlugMustBeUnique(request.Slug);
            await _productBusinessRules.ProductSkuMustBeUnique(request.SKU);
            await _productBusinessRules.DiscountPriceMustBeLessThanPrice(request.Price, request.DiscountPrice);

            var product = _mapper.Map<ProductEntity>(request);
            product.CreatedDate = DateTime.UtcNow;


            await _unitOfWork.GetWriteRepository<ProductEntity>()
                .AddAsync(product);
            await _unitOfWork.SaveAsync();
            return new CreateProductCommandResponse { Id = product.Id };
        }
    }
}
