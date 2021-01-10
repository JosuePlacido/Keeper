using Infrastructure.DAO;
using Infrastructure.Data;
using Domain.Models;

namespace Infrastruture.DAO
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
