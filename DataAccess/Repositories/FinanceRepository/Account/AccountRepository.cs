using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccess.Repositories.FinanceRepository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
