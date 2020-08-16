using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models.Entities;

namespace DataAccess.Repositories
{
	public interface IRepository<T> where T : BaseEntity
	{
		IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);

		IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

		T GetById(int id);

		Task<T> GetByIdAsync(int id);

		T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

		Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

		int Count(Expression<Func<T, bool>> predicate = null);

		Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);

		bool IsExist(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

		Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

		void Add(T entity);

		Task AddAsync(T entity);

		void AddRange(IEnumerable<T> entities);

		Task AddRangeAsync(IEnumerable<T> entities);

		void Update(T entity);

		bool Delete(int id);

		void Delete(T entity);

		void DeleteRange(IEnumerable<T> entities);
	}
}
