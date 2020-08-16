using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccess.Repositories.FinanceRepository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
