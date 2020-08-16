using System;
using System.Threading.Tasks;
using DataAccess.Repositories;
using DataAccess.Repositories.FinanceRepository;
using DataAccess.Repositories.RolePermissionRepository;
using DataAccess.Repositories.UserRepository;
using Models.Entities;

namespace DataAccess.UnitofWork
{
	public interface IUnitOfWork : IDisposable
	{
		/// <summary> 
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		IRepository<T> GetRepository<T>() where T : BaseEntity;

		// Repositories
		IUserRepository UserRepository  { get; }
		IRolePermissionRepository RolePermissionRepository  { get; }
		IAccountRepository AccountRepository  { get; }
		IInvoiceRepository InvoiceRepository  { get; }
		IPaymentRepository PaymentRepository  { get; }
		ICashFlowRepository CashFlowRepository  { get; }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		int SaveChanges();

		/// <summary>
		/// 
		/// </summary>
		Task<int> SaveChangesAsync();

		/// <summary>
		/// 
		/// </summary>
		void Begin();

		/// <summary>
		/// 
		/// </summary>
		Task BeginAsync();

		/// <summary>
		/// 
		/// </summary>
		void Commit();

		/// <summary>
		/// 
		/// </summary>
		Task CommitAsync();

		/// <summary>
		/// 
		/// </summary>
		void Rollback();

		/// <summary>
		/// 
		/// </summary>
		Task RollbackAsync();
	}
}
