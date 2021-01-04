using Domain.Repository;
using Domain.Models;
using Infrastructure.Data;
namespace Infrastruture.Repositorys
{
	public class RepositoryGroup : RepositoryBase<Group>, IRepositoryGroup
	{
		private readonly ApplicationContext _context;
		public RepositoryGroup(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
