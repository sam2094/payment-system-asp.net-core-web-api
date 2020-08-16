using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Extensions;
using Models.Entities;

namespace DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
		private readonly DbContext _dbContext;
		private readonly DbSet<T> _dbSet;

		public GenericRepository(DbContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = _dbContext.Set<T>();
		}
		#region IGenericRepository Members
		public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes)
		{
			return Include(includes);
		}

		public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
		{
			return Include(includes).Where(predicate);
		}

		public T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
		{
			return Include(includes).FirstOrDefault(predicate);
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
		{
			return await Include(includes).FirstOrDefaultAsync(predicate);
		}

		public int Count(Expression<Func<T, bool>> predicate = null)
		{
			return predicate == null ? _dbSet.Count() : _dbSet.Count(predicate);
		}

		public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
		{
			return predicate == null ? await _dbSet.CountAsync() : await _dbSet.CountAsync(predicate);
		}

		public bool IsExist(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
		{
			return Include(includes).Any(predicate);
		}

		public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
		{
			return await Include(includes).AnyAsync(predicate);
		}

		public void Add(T entity)
		{
			_dbSet.Add(entity);
		}

		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public void AddRange(IEnumerable<T> entities)
		{
			_dbSet.AddRange(entities);
		}
		public async Task AddRangeAsync(IEnumerable<T> entities)
		{
			await _dbSet.AddRangeAsync(entities);
		}

		public void Delete(T entity)
		{
			if (entity == null) return;
			_dbSet.Remove(entity);
		}

		public void DeleteRange(IEnumerable<T> entities)
		{
			if (entities.Count() < 1) return;
			_dbSet.RemoveRange(entities);
		}

		private IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = _dbSet;
			includes.ForEach(includeItem => query = query.Include(includeItem));
			return query;
		}
		#endregion
	}
}
