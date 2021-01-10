using Infrastructure.Data;
using Domain.Models;

namespace Infrastructure.DAO
{
	public class DAOChampionship : DAOBase<Championship>
	{
		private readonly ApplicationContext _context;
		public DAOChampionship(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
