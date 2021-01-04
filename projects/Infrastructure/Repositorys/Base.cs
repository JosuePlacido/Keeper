using Domain.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastruture.Repositorys
{
	public abstract class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
	{
		private readonly ApplicationContext _context;

		public RepositoryBase(ApplicationContext Context)
		{
			_context = Context;
		}

		public virtual void Add(TEntity obj)
		{
			_context.Set<TEntity>().Add(obj);
			_context.SaveChanges();
		}

		public virtual async Task<TEntity> GetById(string id)
		{
			return await _context.Set<TEntity>().FindAsync(id);
		}

		public virtual async Task<TEntity[]> GetAll()
		{
			return await _context.Set<TEntity>().ToArrayAsync();
		}

		public virtual void Update(TEntity obj)
		{
			_context.Entry(obj).State = EntityState.Modified;
			_context.SaveChanges();
		}

		public virtual void Remove(TEntity obj)
		{
			_context.Set<TEntity>().Remove(obj);
			_context.SaveChanges();
		}

		public virtual void Dispose()
		{
			_context.Dispose();
		}
	}
}
