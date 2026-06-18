using ETicaret.API.Attributes;
using ETicaret.Application.Features.Orders.Commands.CreateOrderFromBasket;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaret.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [JwtAuthorize]
        [HttpPost("from-basket")]
        public async Task<IActionResult> CreateFromBasket(CreateOrderFromBasketCommandRequest request)
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
