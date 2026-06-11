
using ETicaret.Application.Common.Abstractions.AutoMapper;
using ETicaret.Application.Common.Enums;
using ETicaret.Application.Features.Users.Rules;
using ETicaret.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ETicaret.Application.Common.Exceptions;

namespace ETicaret.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<AppUserEntity> _userManager;
        private readonly IUserBusinessRules _userBusinessRules;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(
         UserManager<AppUserEntity> userManager,
         IMapper mapper, IUserBusinessRules userBusinessRules)
        {
            _userBusinessRules = userBusinessRules;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserEmailMustBeUnique(request.Email);
            await _userBusinessRules.UserNameMustBeUnique(request.UserName);

            var user = _mapper.Map<AppUserEntity>(request);
            user.IsActive = true;
            user.CreatedDate = DateTime.UtcNow;

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .Select(x => x.Description)
                    .ToList();

                throw new ValidationException(errors);
            }
            var roleResult = await _userManager.AddToRoleAsync(user, RoleNames.Customer);

            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                var errors = roleResult.Errors
                      .Select(x => x.Description)
                      .ToList();
                throw new ValidationException(errors);
            }
            return new CreateUserCommandResponse
            {
                Id = user.Id

            };
        }
    }
}
