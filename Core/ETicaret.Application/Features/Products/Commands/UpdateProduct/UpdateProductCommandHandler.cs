using ETicaret.Application.Common.Abstractions.AutoMapper;
using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Products.Rules;
using ETicaret.Domain.Entities.Catalog;
using MediatR;

namespace ETicaret.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductBusinessRules _productBusinessRules;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IProductBusinessRules productBusinessRules, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _productBusinessRules = productBusinessRules;
            _mapper = mapper;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productBusinessRules.ProductMustExist(request.Id);

            await _productBusinessRules.ProductCategoryMustExist(request.CategoryId);
            await _productBusinessRules.ProductSlugMustBeUniqueForUpdate(request.Id, request.Slug);
            await _productBusinessRules.ProductSkuMustBeUniqueForUpdate(request.Id, request.SKU);
            await _productBusinessRules.DiscountPriceMustBeLessThanPrice(request.Price, request.DiscountPrice);

            _mapper.Map(request,product);
            product.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.GetWriteRepository<ProductEntity>().Update(product);
            await _unitOfWork.SaveAsync();

            return new UpdateProductCommandResponse
            {
                Id = product.Id
            };
        }
    }
}
