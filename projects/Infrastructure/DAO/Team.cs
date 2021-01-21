using Keeper.Domain.Models;
using Keeper.Infrastructure.DAO;
using Keeper.Infrastructure.Data;

namespace Keeper.Infrastruture.DAO
{
	public class DAOTeam : DAOBase<Team>
	{
		private readonly ApplicationContext _context;
		public DAOTeam(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
