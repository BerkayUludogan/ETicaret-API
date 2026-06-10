//using ETicaret.Application.Features.Users.DTOs;
//using ETicaret.Application.Helper;
//using ETicaret.Application.Shared.Abstractions.AutoMapper;
//using ETicaret.Application.Shared.Abstractions.Services;
//using ETicaret.Application.Shared.Abstractions.UnitOfWorks;
//using ETicaret.Domain.Entities.Identity;
//using ETicaret.Persistence.Context;
//using Microsoft.AspNetCore.Identity;

//namespace ETicaret.Persistence.Services
//{
//    public class UserService(
//        UserManager<AppUserEntity> _userManager,
//        RoleManager<AppRoleEntity> _roleManager,
//        IUnitOfWork _unitOfWork,
//        IMapper _mapper,
//        ETicaretContext _context
//        ) : IUserService
//    {
//        public async Task<Guid> CreateAsync(UserResponseDto userDto)
//        {
//            if (ReservedNameChecker.IsReservedUserName(userDto.UserName))
//                throw new Exception("Kullanıcı adı yasaklı!");

//            var user = _mapper.Map<AppUserEntity>(userDto);

//            var result = await _userManager.CreateAsync(user,userDto.Password);

//            if (!result.Succeeded)
//            {
//                var errors = string.Join(" | ", result.Errors.Select(x => x.Description));
//                throw new Exception(errors);
//            }
//            //Rol ekleme
//            if (!string.IsNullOrEmpty(userDto.RoleName))
//            {
//                var roleResult = await _userManager.AddToRoleAsync(user, userDto.RoleName);

//                if (!roleResult.Succeeded)
//                {
//                    var errors = string.Join(" | ", roleResult.Errors.Select(x => x.Description));
//                    throw new Exception(errors);
//                }
//            }
//            return user.Id;
//        }
//    }
//}
