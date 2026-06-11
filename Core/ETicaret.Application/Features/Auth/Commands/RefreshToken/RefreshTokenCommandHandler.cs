using ETicaret.Application.Common.Abstractions.Token;
using ETicaret.Application.Common.Exceptions.Errors;
using ETicaret.Application.Features.Auth.Rules;
using ETicaret.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
    {
        private readonly UserManager<AppUserEntity> _userManager;
        private readonly IAuthBusinessRules _authBusinessRules;
        private readonly ITokenService _tokenService;
        public RefreshTokenCommandHandler(UserManager<AppUserEntity> userManager, IAuthBusinessRules authBusinessRules, ITokenService tokenService)
        {
            _userManager = userManager;
            _authBusinessRules = authBusinessRules;
            _tokenService = tokenService;
        }

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _authBusinessRules.UserRefreshTokenMustExist(request.RefreshToken);

            await _authBusinessRules.RefreshTokenMustNotBeExpired(user);
            await _authBusinessRules.UserMustBeActive(user);

            var roles = await _userManager.GetRolesAsync(user);
            var token = await _tokenService.CreateAccessToken(user, roles);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenEndDate = token.RefreshTokenExpiration;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                throw new Common.Exceptions.ValidationException(AuthErrors.RefreshTokenNotSaved);

            return new RefreshTokenCommandResponse { Token = token };
        }
    }
}
