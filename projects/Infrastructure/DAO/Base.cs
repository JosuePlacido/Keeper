using Infrastructure.Data;
using Application.Contract.DAL;

namespace Infrastructure.DAO
{
	public abstract class DAO : IDAO
	{
		protected readonly ApplicationContext _context;
		public DAO(ApplicationContext Context)
		{
			_context = Context;
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
