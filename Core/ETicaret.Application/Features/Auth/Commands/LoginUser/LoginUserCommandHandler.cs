
using ETicaret.Application.Common.Abstractions.Services;
using ETicaret.Application.Common.Abstractions.Token;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Application.Features.Auth.DTOs;
using ETicaret.Application.Features.Auth.Rules;
using ETicaret.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IAuthBusinessRules _authBusinessRules;
        private readonly UserManager<AppUserEntity> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthAuditService _authAuditService;

        public LoginUserCommandHandler(
            UserManager<AppUserEntity> userManager,
            ITokenService tokenService,
            IAuthBusinessRules authBusinessRules,
            IHttpContextAccessor httpContextAccessor,
            IAuthAuditService authAuditService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _authBusinessRules = authBusinessRules;
            _httpContextAccessor = httpContextAccessor;
            _authAuditService = authAuditService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

            AppUserEntity? user = null;

           
            try
            {
                user = await _authBusinessRules.UserMustExistByEmail(request.Email);

                await _authBusinessRules.UserMustBeActive(user);
                await _authBusinessRules.UserMustNotBeLockedOut(user);
                await _authBusinessRules.UserPasswordMustBeValid(user, request.Password);
             
                var roles = await _userManager.GetRolesAsync(user);
                var token = await _tokenService.CreateAccessToken(user, roles);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = token.RefreshTokenExpiration;
                var updateResult = await _userManager.UpdateAsync(user);

                if (!updateResult.Succeeded)
                {
                    var errors = updateResult.Errors
                        .Select(x => x.Description).ToList();
                    throw new Common.Exceptions.ValidationException(errors);
                }

                await _authAuditService.LogLoginAttemptAsync(new UserLoginAuditDto
                {

                    Email = request.Email,
                    IPAddress = ipAddress,
                    LoginTime = DateTime.UtcNow,
                    Success = true,
                    UserId = user.Id
                });

                return new LoginUserCommandResponse
                {
                    Token = token,
                    User = new AuthUserDto
                    {
                        Id = user.Id,
                        UserName = user.UserName!,
                        Email = user.Email!,
                        Roles = roles.ToList()

                    }
                };
            }
            catch (Exception)
            {
                await _authAuditService.LogLoginAttemptAsync(new UserLoginAuditDto
                {
                    Email = request.Email,
                    IPAddress = ipAddress,
                    LoginTime = DateTime.UtcNow,
                    Success = false,
                    UserId = user?.Id,
                    FailureReason = AuthErrors.InvalidCredentials
                });
                throw;
            }

        }
    }
}
