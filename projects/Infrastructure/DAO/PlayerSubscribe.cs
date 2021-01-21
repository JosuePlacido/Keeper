using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;

namespace Keeper.Infrastructure.DAO
{
	public class DAOPlayerSubscribe : DAOBase<PlayerSubscribe>
	{
		private readonly ApplicationContext _context;
		public DAOPlayerSubscribe(ApplicationContext Context)
			: base(Context) => _context = Context;

	}
}
