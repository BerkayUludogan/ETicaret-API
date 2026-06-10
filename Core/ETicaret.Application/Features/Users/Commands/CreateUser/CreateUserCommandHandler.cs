
using ETicaret.Application.Common.Abstractions.Caching;
using ETicaret.Application.Shared.Abstractions.AutoMapper;
using ETicaret.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<AppUserEntity> _userManager;
        //private readonly IUserBusinessRules _userBusinessRules;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public CreateUserCommandHandler(
         UserManager<AppUserEntity> userManager, 
         IMapper mapper,
         ICacheService cacheService)
        {
            _userManager = userManager; 
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
