using ETicaret.Application.Abstractions.AutoMapper;
using ETicaret.Application.Abstractions.Services;
using ETicaret.Application.Abstractions.UnitOfWorks;
using ETicaret.Application.DTOs.User;
using ETicaret.Application.Helper;
using ETicaret.Domain.Entities.Identity;
using ETicaret.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Persistence.Services
{
    public class UserService(
        UserManager<AppUserEntity> _userManager,
        RoleManager<AppRoleEntity> _roleManager,
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        ETicaretContext _context
        ) : IUserService
    {
        public async Task<Guid> CreateAsync(CreateUserDto userDto)
        {
            // TODO : Exceptionları düzelt !!!
            try
            {
                AppUserEntity user = _mapper.Map<AppUserEntity>(userDto);
                if (ReservedNameChecker.IsReservedUserName(userDto.UserName)) 
                    throw new Exception("Ayrı yazılmış kullanıcı adı hatası.");

                await _unitOfWork.BeginTransactionAsync();

                IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);
                if (!result.Succeeded)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    throw new Exception(" Kimlik doğrulama hatası.");
                }
                await _userManager.UpdateSecurityStampAsync(user);
                try
                {
                    // Set Role
                    var roleResult = await _userManager.AddToRoleAsync(user, userDto.RoleName);

                    if (!roleResult.Succeeded) 
                        throw new Exception(" Kimlik doğrulama hatası.");


                    // Complete transaction
                    await _unitOfWork.CommitTransactionAsync();
                }
                catch
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    throw new Exception(" Kullanıcı oluşturulamadı.");
                    
                }

                return user.Id;

            }
            catch (Exception ex)
            {
                throw new Exception("Beklenmeyen bir hata oluştu.");
            }
        }
    }
}
