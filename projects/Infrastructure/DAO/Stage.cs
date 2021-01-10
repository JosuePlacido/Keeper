using Infrastructure.DAO;
using Infrastructure.Data;
using Domain.Models;

namespace Infrastruture.DAO
{
	public class DAOStage : DAOBase<Stage>
	{
		private readonly ApplicationContext _context;
		public DAOStage(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
