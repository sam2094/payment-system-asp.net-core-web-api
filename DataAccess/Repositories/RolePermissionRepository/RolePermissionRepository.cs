using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccess.Repositories.RolePermissionRepository
{
    public class RolePermissionRepository : GenericRepository<RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
