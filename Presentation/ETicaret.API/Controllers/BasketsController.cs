
using ETicaret.API.Attributes;
using ETicaret.API.Extensions;
using ETicaret.Application.Features.Baskets.Commands.AddBasketItem;
using ETicaret.Application.Features.Baskets.Commands.ClearBasket;
using ETicaret.Application.Features.Baskets.Commands.RemoveBasketItem;
using ETicaret.Application.Features.Baskets.Commands.UpdateBasketItemQuantity;
using ETicaret.Application.Features.Baskets.Queries.GetMyBasket;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var response = await _mediator.Send(new GetMyBasketQueryRequest
            {
                UserId = userId.Value
            });
            return Ok(response);
        }
        [JwtAuthorize]
        [HttpPost("items")]
        public async Task<IActionResult> AddItem(AddBasketItemCommandRequest request)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            request.UserId = userId.Value;
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        [JwtAuthorize]
        [HttpPut("items/{basketItemId:guid}")]
        public async Task<IActionResult> UpdateItemQuantity(
          [FromRoute] Guid basketItemId,
          [FromBody] UpdateBasketItemQuantityCommandRequest request)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            request.UserId = userId.Value;
            request.BasketItemId = basketItemId;

            var response = await _mediator.Send(request);

            return Ok(response);
        }
        [JwtAuthorize]
        [HttpDelete("items/{basketItemId:guid}")]
        public async Task<IActionResult> Delete(Guid basketItemId)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var response = await _mediator.Send(new RemoveBasketItemCommandRequest
            {
                UserId = userId.Value,
                BasketItemId = basketItemId
            });

            return Ok(response);
        }
        [JwtAuthorize]
        [HttpDelete("items")]
        public async Task<IActionResult> ClearBasket()
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var response = await _mediator.Send(new ClearBasketCommandRequest
            {
                UserId = userId.Value
            });
            return Ok(response);
        }
    }
}
