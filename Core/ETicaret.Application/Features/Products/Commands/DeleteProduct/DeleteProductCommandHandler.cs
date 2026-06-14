using ETicaret.Application.Common.Abstractions.UnitOfWorks;
using ETicaret.Application.Features.Products.Rules;
using ETicaret.Domain.Entities.Catalog;
using MediatR;

namespace ETicaret.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductBusinessRules _productBusinessRules;

        public DeleteProductCommandHandler(
            IUnitOfWork unitOfWork,
            IProductBusinessRules productBusinessRules)
        {
            _unitOfWork = unitOfWork;
            _productBusinessRules = productBusinessRules;
        }
        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productBusinessRules.ProductMustExist(request.Id);

            product.IsDeleted = true;
            product.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.GetWriteRepository<ProductEntity>().Update(product);
            await _unitOfWork.SaveAsync();

            return new DeleteProductCommandResponse
            {
                Id = product.Id
            };

        }
    }
}
