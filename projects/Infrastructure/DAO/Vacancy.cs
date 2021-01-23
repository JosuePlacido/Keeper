using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;

namespace Keeper.Infrastructure.DAO
{
	public class DAOVacancy : DAOBase<Vacancy>
	{
		public DAOVacancy(ApplicationContext Context) : base(Context) { }
	}
}
