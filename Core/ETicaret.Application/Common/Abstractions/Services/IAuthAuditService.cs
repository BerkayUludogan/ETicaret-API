using ETicaret.Application.Features.Auth.DTOs;

namespace ETicaret.Application.Common.Abstractions.Services
{
    public interface IAuthAuditService
    {
        Task LogLoginAttemptAsync(UserLoginAuditDto auditDto);
    }
}
