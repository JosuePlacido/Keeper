using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;

namespace Keeper.Infrastructure.DAO
{
	public class DAOVacancy : DAOBase<Vacancy>
	{
		private readonly ApplicationContext _context;
		public DAOVacancy(ApplicationContext Context)
			: base(Context) => _context = Context;
	}
}
