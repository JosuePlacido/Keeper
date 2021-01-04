using Domain.Repository;
using Domain.Models;
using Infrastructure.Data;
namespace Infrastruture.Repositorys
{
	public class RepositoryStatistics : RepositoryBase<Statistics>, IRepositoryStatistics
	{
		private readonly ApplicationContext _context;
		public RepositoryStatistics(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
