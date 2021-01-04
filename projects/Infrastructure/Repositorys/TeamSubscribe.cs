using Domain.Repository;
using Domain.Models;
using Infrastructure.Data;
namespace Infrastruture.Repositorys
{
	public class RepositoryTeamSubscribe : RepositoryBase<TeamSubscribe>, IRepositoryTeamSubscribe
	{
		private readonly ApplicationContext _context;
		public RepositoryTeamSubscribe(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
