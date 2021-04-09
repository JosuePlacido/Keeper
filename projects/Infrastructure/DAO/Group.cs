using System.Linq;
using System.Threading.Tasks;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Keeper.Infrastructure.DAO;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Keeper.Application.Contract;
using Keeper.Domain.Utils;
using System.Collections.Generic;
using Keeper.Domain.Enum;
using Keeper.Domain.Core;
using System;

namespace Keeper.Infrastructure.DAO
{
	public class DAOGroup : DAO, IDAOGroup
	{
		public DAOGroup(ApplicationContext Context) : base(Context) { }

		public async Task<Group> GetByIdWithStatistics(string id)
		{
			return await _context.Groups.AsNoTracking().Where(g => g.Id == id)
				.Include(g => g.Statistics.OrderBy(s => s.Position))
				.FirstOrDefaultAsync();
		}

		public async Task<Group> GetByIdWithStatisticsAndTeamSubscribe(string group)
		{
			return await _context.Groups.AsNoTracking().Where(g => g.Id == group)
				.Include(g => g.Statistics)
					.ThenInclude(s => ((Statistic)s).TeamSubscribe)
				.FirstOrDefaultAsync();
		}

		public async Task SetTeamOnVacancy(Vacancy vacancy, string teamSubscribeId)
		{
			Match[] matches = await _context.Matchs.AsNoTracking()
				.Where(m => m.VacancyAwayId == vacancy.Id || m.VacancyHomeId == vacancy.Id)
				.ToArrayAsync();
			foreach (var match in matches)
			{
				match.SetTeam(teamSubscribeId, match.VacancyHomeId == vacancy.Id);
				_context.Matchs.Update(match);
				//_context.Vacancys.Remove(vacancy);
			}
		}

		public void Update(Group group)
		{
			_context.Groups.Update(group);
		}
		public async Task<bool> HasPenndentMatchesWithDateInRound(Group group)
		{
			return await _context.Matchs.AsNoTracking().AnyAsync(m => m.GroupId == group.Id
				&& m.Status != Status.Finish
				&& m.Date != null && m.Round == group.CurrentRound);
		}

		public async Task<bool> IsOpenGroup(string group)
		{
			return await _context.Matchs.AsNoTracking().AnyAsync(m => m.GroupId == group && m.Status
				!= Status.Finish);
		}
	}
}
