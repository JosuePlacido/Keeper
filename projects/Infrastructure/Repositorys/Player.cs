using Domain.Repository;
using Domain.Models;
using Infrastructure.Data;
namespace Infrastruture.Repositorys
{
	public class RepositoryPlayer : RepositoryBase<Player>, IRepositoryPlayer
	{
		private readonly ApplicationContext _context;
		public RepositoryPlayer(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
