using Infrastructure.DAO;
using Infrastructure.Data;
using Domain.Models;

namespace Infrastruture.DAO
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
