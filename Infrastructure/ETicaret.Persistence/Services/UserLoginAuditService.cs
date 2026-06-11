using ETicaret.Application.Common.Abstractions.Services;
using ETicaret.Application.Features.Auth.DTOs;
using ETicaret.Domain.Entities.Auth;
using ETicaret.Persistence.Context;

namespace ETicaret.Persistence.Services
{
    public class UserLoginAuditService : IAuthAuditService
    {
        private readonly ETicaretContext _context;
        public UserLoginAuditService(ETicaretContext context)
        {
            _context = context;
        }

        public async Task LogLoginAttemptAsync(UserLoginAuditDto auditDto)
        {
            var entity = new UserLoginAuditEntity
            {
                Id = Guid.NewGuid(),
                UserNameOrEmail = auditDto.Email,
                IPAddress = auditDto.IPAddress,
                LoginTime = auditDto.LoginTime,
                Success = auditDto.Success, 
                FailureReason = auditDto.FailureReason,
                UserId = auditDto.UserId,
                CreatedDate = DateTime.UtcNow
            };
            await _context.UserLoginAudits.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
