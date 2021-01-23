using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keeper.Infrastructure.DAO
{
	public abstract class DAOBase<TEntity> : IDisposable, IDAO<TEntity> where TEntity : class
	{
		protected readonly ApplicationContext _context;

		public DAOBase(ApplicationContext Context)
		{
			_context = Context;
		}

		public virtual async Task<TEntity> Add(TEntity obj)
		{
			await _context.Set<TEntity>().AddAsync(obj);
			await _context.SaveChangesAsync();
			return obj;
		}

		public virtual async Task<TEntity> GetById(string id)
		{
			return await _context.Set<TEntity>().FindAsync(id);
		}

		public virtual async Task<TEntity[]> GetAll()
		{
			return await _context.Set<TEntity>().AsNoTracking().ToArrayAsync();
		}

		public virtual async Task<TEntity> Update(TEntity obj)
		{
			_context.Entry(obj).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return obj;
		}

		public virtual async Task<TEntity> Remove(TEntity obj)
		{
			_context.Set<TEntity>().Remove(obj);
			await _context.SaveChangesAsync();
			return obj;
		}

		public virtual void Dispose()
		{
			_context.Dispose();
		}
	}
}
