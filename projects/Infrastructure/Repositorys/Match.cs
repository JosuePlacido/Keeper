using Domain.Repository;
using Domain.Models;
using Infrastructure.Data;
namespace Infrastruture.Repositorys
{
	public class RepositoryMatch : RepositoryBase<Match>, IRepositoryMatch
	{
		private readonly ApplicationContext _context;
		public RepositoryMatch(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
