using System;
using System.Threading.Tasks;
using Domain.Core;
using Keeper.Infrastructure.Data;

namespace Infrastructure.Data
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly ApplicationContext _context;
		public UnitOfWork(ApplicationContext context)
		{
			_context = context;
		}
		public async Task Commit()
		{
			await _context.SaveChangesAsync();
		}
		private bool disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			this.disposed = true;
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
