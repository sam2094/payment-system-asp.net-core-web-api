using System.Threading.Tasks;
using DataAccess.UnitofWork;
using Models.Parameters.User;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Enums.ErrorEnums;
using Common.Resources;
using Models.Entities;

namespace Services.Services.UserServices
{
    public class UserService : AbstractService<AuthorizationOutput>, IUserService<AuthorizationOutput>
    {
        public UserService(IUnitOfWork UoW, bool beginTransaction = false) :
              base(UoW, beginTransaction)
        { }

        public async Task<ContainerResult<AuthorizationOutput>> Authorization(AuthorizationInput input)
        {
            User user = await _uow.UserRepository.GetAsync(x => x.Id == input.CurrentUserId,
                              i => i.Role.RolePermission);

            if (user == null)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.PERMISSION_DOES_NOT_EXIST,
                    ErrorMessage = Resource.PERMISSION_DOES_NOT_EXIST,
                    StatusCode = ErrorHttpStatus.VALIDATION
                });
                return Result;
            }

            List<Permission> userPermissions = _uow.RolePermissionRepository.GetAll(x => x.RoleId == user.RoleId, i => i.Permission, i => i.Role)
                            .Select(x => new Permission
                            {
                                Id = x.Permission.Id,
                                Name = x.Permission.Name,
                                Description = x.Permission.Description,
                                Added = x.Permission.Added
                            }).ToList();

            if (!userPermissions.Any(x => x.Id == input.PermissionId))
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.PERMISSION_DOES_NOT_EXIST,
                    ErrorMessage = Resource.PERMISSION_DOES_NOT_EXIST,
                    StatusCode = ErrorHttpStatus.VALIDATION
                });
                return Result;
            }

            return Result;
        }
    }
}