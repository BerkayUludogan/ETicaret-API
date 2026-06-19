using ETicaret.API.Attributes;
using ETicaret.API.Extensions;
using ETicaret.Application.Features.Payments.Commands.PayOrder;
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
    }
}
