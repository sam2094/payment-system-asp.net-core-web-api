using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccess.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
