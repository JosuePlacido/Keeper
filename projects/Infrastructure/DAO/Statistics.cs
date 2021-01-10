using Infrastructure.DAO;
using Infrastructure.Data;
using Domain.Models;

namespace Infrastruture.DAO
{
	public class DAOStatistics : DAOBase<Statistic>
	{
		private readonly ApplicationContext _context;
		public DAOStatistics(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
