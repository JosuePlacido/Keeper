using Keeper.Domain.Models;
using Keeper.Infrastructure.DAO;
using Keeper.Infrastructure.Data;

namespace Keeper.Infrastruture.DAO
{
	public class DAOPlayer : DAOBase<Player>
	{
		private readonly ApplicationContext _context;
		public DAOPlayer(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
