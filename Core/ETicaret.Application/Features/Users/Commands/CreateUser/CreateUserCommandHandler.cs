
using ETicaret.Application.Common.Abstractions.AutoMapper;
using ETicaret.Application.Features.Users.Rules;
using ETicaret.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

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

                throw new ETicaret.Application.Common.Exceptions.ValidationException(errors);
            }
            return new CreateUserCommandResponse
            {
                Id = user.Id
            };
        }
    }
}
