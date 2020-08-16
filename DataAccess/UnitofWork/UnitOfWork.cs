using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using DataAccess.Repositories;
using Models.Entities;
using DataAccess.Repositories.UserRepository;
using DataAccess.Repositories.RolePermissionRepository;
using DataAccess.Repositories.FinanceRepository;

namespace DataAccess.UnitofWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool disposed = false;
        private readonly DbContext _dbContext;
        public IDbContextTransaction Transaction { get; set; }

        // Repositories
        private IUserRepository userRepository;

        private IRolePermissionRepository rolePermissionRepository;

        private IAccountRepository accountRepository;

        private IInvoiceRepository invoiceRepository;

        private IPaymentRepository paymentRepository;

        private ICashFlowRepository cashFlowRepository;

        // Repositories props
        public IUserRepository UserRepository => userRepository = userRepository ?? new UserRepository(_dbContext);
        public IRolePermissionRepository RolePermissionRepository => rolePermissionRepository = rolePermissionRepository ?? new RolePermissionRepository(_dbContext);
        public IAccountRepository AccountRepository => accountRepository = accountRepository ?? new AccountRepository(_dbContext);
        public IInvoiceRepository InvoiceRepository => invoiceRepository = invoiceRepository ?? new InvoiceRepository(_dbContext);
        public IPaymentRepository PaymentRepository => paymentRepository = paymentRepository ?? new PaymentRepository(_dbContext);
        public ICashFlowRepository CashFlowRepository => cashFlowRepository = cashFlowRepository ?? new CashFlowRepository(_dbContext);

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region IUnitOfWork Members
        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_dbContext);
        }

        public void Begin()
        {
            Transaction = _dbContext.Database.BeginTransaction();
        }

        public async Task BeginAsync()
        {
            Transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public void Commit()
        {
            Transaction.Commit();
        }

        public async Task CommitAsync()
        {
            await Transaction.CommitAsync();
        }

        public void Rollback()
        {
            Transaction.Rollback();
        }

        public async Task RollbackAsync()
        {
            await Transaction.RollbackAsync();
        }

        public int SaveChanges()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IDisposable Members
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
