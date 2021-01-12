using Infrastructure.DAO;
using Infrastructure.Data;
using Domain.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAO
{
	public class DAOMatch : DAOBase<Match>
	{
		private readonly ApplicationContext _context;
		public DAOMatch(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}

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
