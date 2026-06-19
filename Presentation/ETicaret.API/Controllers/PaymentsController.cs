using ETicaret.API.Attributes;
using ETicaret.API.Extensions;
using ETicaret.Application.Features.Payments.Commands.PayOrder;
using ETicaret.Application.Features.Payments.Queries.GetMyPayments;
using ETicaret.Application.Features.Payments.Queries.GetPaymentByOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [JwtAuthorize]
        [HttpPost("pay-order")]
        public async Task<IActionResult> PayOrder(PayOrderCommandRequest request)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            request.UserId = userId.Value;

            var response = await _mediator.Send(request);

            return Ok(response);
        }
        [JwtAuthorize]
        [HttpGet("my-payments")]
        public async Task<IActionResult> GetMyPayments()
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var response = await _mediator.Send(new GetMyPaymentsQueryRequest
            {
                UserId = userId.Value
            });

            return Ok(response);
        }
        [JwtAuthorize]
        [HttpGet("order/{orderId:guid}")]
        public async Task<IActionResult> GetPaymentByOrder(Guid orderId)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var response = await _mediator.Send(new GetPaymentByOrderQueryRequest
            {
                UserId = userId.Value,
                OrderId = orderId
            });

            return Ok(response);
        }
    }
}