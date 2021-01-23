using System.Linq;
using Microsoft.EntityFrameworkCore;
using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;

namespace Keeper.Infrastructure.DAO
{
	public class DAOMatch : DAOBase<Match>
	{
		public DAOMatch(ApplicationContext Context) : base(Context) { }

		internal Match[] GetByGroupWithTeams(string group)
		{
			return _context.Matchs.Where(mtc => mtc.GroupId == group)
				.Include(mtc => mtc.Home).ThenInclude(ts => ts.Team)
				.Include(mtc => mtc.Away).ThenInclude(ts => ts.Team)
				.Include(mtc => mtc.VacancyHome)
				.Include(mtc => mtc.VacancyAway)
				.OrderBy(mtc => mtc.Round).ToArray();
		}
	}
}
