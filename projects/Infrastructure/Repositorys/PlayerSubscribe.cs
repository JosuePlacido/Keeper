using Domain.Repository;
using Domain.Models;
using Infrastructure.Data;
namespace Infrastruture.Repositorys
{
	public class RepositoryPlayerSubscribe : RepositoryBase<PlayerSubscribe>, IRepositoryPlayerSubscribe
	{
		private readonly ApplicationContext _context;
		public RepositoryPlayerSubscribe(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
