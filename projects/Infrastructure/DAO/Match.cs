using Infrastructure.DAO;
using Infrastructure.Data;
using Domain.Models;

namespace Infrastruture.DAO
{
	public class DAOMatch : DAOBase<Match>
	{
		private readonly ApplicationContext _context;
		public DAOMatch(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
