using Domain.Core;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Contract.Repository;

namespace Infrastructure.Repository
{
	public abstract class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : Entity
	{
		protected readonly ApplicationContext _context;

		public RepositoryBase(ApplicationContext Context)
		{
			_context = Context;
		}

		public virtual async Task<TEntity> Add(TEntity obj)
		{
			await _context.Set<TEntity>().AddAsync(obj);
			return obj;
		}

		public virtual async Task<TEntity> GetById(string id)
		{
			return await _context.Set<TEntity>().AsNoTracking()
				.Where(t => t.Id == id).FirstOrDefaultAsync();
		}

		public virtual async Task<TEntity[]> GetAll()
		{
			return await _context.Set<TEntity>().AsNoTracking().ToArrayAsync();
		}

		public virtual async Task<TEntity> Update(TEntity obj)
		{
			await Task.Run(() => _context.Entry(obj).State = EntityState.Modified);
			return obj;
		}

		public virtual async Task<TEntity> Remove(TEntity obj)
		{
			await Task.Run(() => _context.Set<TEntity>().Remove(obj));
			return obj;
		}

		public virtual void Dispose()
		{
			_context.Dispose();
		}
	}
}
