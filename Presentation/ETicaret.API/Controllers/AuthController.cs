using ETicaret.API.Attributes;
using ETicaret.Application.Features.Auth.Commands.LoginUser;
using ETicaret.Application.Features.Auth.Commands.Logout;
using ETicaret.Application.Features.Auth.Commands.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaret.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommandRequest refreshTokenCommandRequest)
        {
            RefreshTokenCommandResponse response = await _mediator.Send(refreshTokenCommandRequest);
            return Ok(response);
        }
        [JwtAuthorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            await _mediator.Send(new LogoutCommandRequest { UserId = Guid.Parse(userId) });
            return NoContent();
        }
    }
}
