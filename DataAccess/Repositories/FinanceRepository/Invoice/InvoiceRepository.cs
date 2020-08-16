using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccess.Repositories.FinanceRepository
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
