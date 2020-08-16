using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccess.Repositories.FinanceRepository
{
    public class CashFlowRepository : GenericRepository<CashFlow>, ICashFlowRepository
    {
        public CashFlowRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
