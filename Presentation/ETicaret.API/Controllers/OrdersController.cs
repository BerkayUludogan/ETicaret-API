using ETicaret.API.Attributes;
using ETicaret.API.Extensions;
using ETicaret.Application.Common.Enums;
using ETicaret.Application.Features.Orders.Commands.CancelOrder;
using ETicaret.Application.Features.Orders.Commands.CreateOrderFromBasket;
using ETicaret.Application.Features.Orders.Commands.UpdateOrderStatus;
using ETicaret.Application.Features.Orders.Queries.GetMyOrders;
using ETicaret.Application.Features.Orders.Queries.GetOrderById;
using ETicaret.Application.Features.Orders.Queries.GetOrders;
using ETicaret.Application.Features.Orders.Queries.GetOrderStatusHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            request.UserId = userId.Value;
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        [JwtAuthorize(RoleNames.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetOrdersQueryRequest());

            return Ok(response);
        }
        [JwtAuthorize]
        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var response = await _mediator.Send(new GetMyOrdersQueryRequest
            {
                UserId = userId.Value
            });

            return Ok(response);
        }
        [JwtAuthorize]
        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetById(Guid orderId)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var response = await _mediator.Send(new GetOrderByIdQueryRequest
            {
                OrderId = orderId,
                UserId = userId.Value,
                IsAdmin = User.IsInRole(RoleNames.Admin)
            });

            return Ok(response);
        }
        [JwtAuthorize(RoleNames.Admin)]
        [HttpPut("{orderId:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid orderId, UpdateOrderStatusCommandRequest request)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            request.OrderId = orderId;
            request.ChangedByUserId = userId.Value;

            var response = await _mediator.Send(request);

            return Ok(response);
        }
        [JwtAuthorize(RoleNames.Admin)]
        [HttpPut("{orderId:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid orderId)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var response = await _mediator.Send(new CancelOrderCommandRequest
            {
                OrderId = orderId,
                ChangedByUserId = userId.Value
            });

            return Ok(response);
        }
        [JwtAuthorize(RoleNames.Admin)]
        [HttpGet("{orderId:guid}/history")]
        public async Task<IActionResult> GetHistory(Guid orderId)
        {
            var response = await _mediator.Send(new GetOrderStatusHistoryQueryRequest
            {
                OrderId = orderId
            });

            return Ok(response);
        }
    }
}