using Infrastructure.DAO;
using Infrastructure.Data;
using Domain.Models;

namespace Infrastruture.DAO
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
