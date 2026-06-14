using ETicaret.API.Attributes;
using ETicaret.Application.Common.Enums;
using ETicaret.Application.Features.Products.Commands.CreateProduct;
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
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetProductsQueryRequest());
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetProductByIdQueryRequest { Id = id });
            return Ok(response);
        }
    }
}
