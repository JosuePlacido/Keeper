using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;

namespace Keeper.Infrastructure.DAO
{
	public class DAOTeamSubscribe : DAOBase<TeamSubscribe>
	{
		private readonly ApplicationContext _context;
		public DAOTeamSubscribe(ApplicationContext Context)
			: base(Context) => _context = Context;

	}
}
