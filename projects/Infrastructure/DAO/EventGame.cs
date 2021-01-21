using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;

namespace Keeper.Infrastructure.DAO
{
	public class DAOEventGame : DAOBase<EventGame>
	{
		private readonly ApplicationContext _context;
		public DAOEventGame(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
