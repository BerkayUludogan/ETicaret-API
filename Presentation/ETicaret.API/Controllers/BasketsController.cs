
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ETicaret.API.Attributes;
using System.Security.Claims;
using ETicaret.Application.Features.Baskets.Queries.GetMyBasket;
using ETicaret.Application.Features.Baskets.Commands.AddBasketItem;
namespace ETicaret.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [JwtAuthorize]
        [HttpGet("my-basket")]
        public async Task<IActionResult> GetMyBasket()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            var response = await _mediator.Send(new GetMyBasketQueryRequest
            {
                UserId = Guid.Parse(userId)
            });
            return Ok(response);
        }
        [JwtAuthorize]
        [HttpPost("items")]
        public async Task<IActionResult> AddItem(AddBasketItemCommandRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            request.UserId = Guid.Parse(userId);

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }

}
