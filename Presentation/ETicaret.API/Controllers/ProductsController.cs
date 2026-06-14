using ETicaret.API.Attributes;
using ETicaret.Application.Common.Enums;
using ETicaret.Application.Features.Products.Commands.CreateProduct;
using ETicaret.Application.Features.Products.Commands.DeleteProduct;
using ETicaret.Application.Features.Products.Commands.UpdateProduct;
using ETicaret.Application.Features.Products.Queries.GetProductById;
using ETicaret.Application.Features.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [JwtAuthorize(RoleNames.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommandRequest request)
        {
            CreateProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetProductsQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetProductByIdQueryRequest { Id = id });
            return Ok(response);
        }

        [JwtAuthorize(RoleNames.Admin)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductCommandRequest request)
        {
            request.Id = id;
            UpdateProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [JwtAuthorize(RoleNames.Admin)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteProductCommandRequest
            {
                Id = id
            });

            return Ok(response);
        }
    }
}
