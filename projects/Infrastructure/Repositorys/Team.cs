using Domain.Repository;
using Domain.Models;
using Infrastructure.Data;
namespace Infrastruture.Repositorys
{
	public class RepositoryTeam : RepositoryBase<Team>, IRepositoryTeam
	{
		private readonly ApplicationContext _context;
		public RepositoryTeam(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
