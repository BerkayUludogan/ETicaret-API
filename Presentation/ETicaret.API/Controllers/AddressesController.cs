using ETicaret.API.Attributes;
using ETicaret.API.Extensions;
using ETicaret.Application.Features.Addresses.Commands.CreateAddress;
using ETicaret.Application.Features.Addresses.Commands.DeleteAddress;
using ETicaret.Application.Features.Addresses.Commands.UpdateAddress;
using ETicaret.Application.Features.Addresses.Queries.GetMyAddresses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [JwtAuthorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAddressCommandRequest request)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            request.UserId = userId.Value;

            var response = await _mediator.Send(request);

            return Ok(response);
        }
        [JwtAuthorize]
        [HttpGet("my-addresses")]
        public async Task<IActionResult> GetMyAddresses()
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var response = await _mediator.Send(new GetMyAddressesQueryRequest
            {
                UserId = userId.Value
            });

            return Ok(response);
        }
        [JwtAuthorize]
        [HttpPut("{addressId:guid}")]
        public async Task<IActionResult> Update(Guid addressId, UpdateAddressCommandRequest request)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            request.UserId = userId.Value;
            request.AddressId = addressId;

            var response = await _mediator.Send(request);

            return Ok(response);
        }
        [JwtAuthorize]
        [HttpDelete("{addressId:guid}")]
        public async Task<IActionResult> Delete(Guid addressId)
        {
            var userId = User.GetUserId();

            if (userId is null)
                return Unauthorized();

            var result = await _mediator.Send(new DeleteAddressCommandRequest
            {
                UserId = userId.Value,
                AddressId = addressId
            });
            return Ok(result);
        }
    }
}