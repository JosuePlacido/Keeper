using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Keeper.Domain.Models;
using Keeper.Infrastructure.DAO;
using Keeper.Infrastructure.Data;

namespace Keeper.Infrastructure.DAO
{
	public class DAOChampionship : DAOBase<Championship>, IDAOChampionship
	{
		private readonly ApplicationContext _context;
		public DAOChampionship(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}

		public override async Task<Championship> Add(Championship obj)
		{
			await base.Add(obj);
			await _context.Entry(obj).Collection(c => c.Stages).Query()
					.Include(stg => ((Stage)stg).Groups)
						.ThenInclude(grp => ((Group)grp).Matchs)
							.ThenInclude(mtc => ((Match)mtc).Home.Team)
				.Include(chp => chp.Groups)
						.ThenInclude(grp => ((Group)grp).Matchs)
							.ThenInclude(mtc => ((Match)mtc).Away.Team)
							.LoadAsync();
			return obj;
		}
		public string[] VerifyCreatedIds(Championship championship)
		{
			var invalids = new List<string>();
			var teams = championship.Teams.Select(ts => ts.Id).ToArray();
			var players = championship.Teams.SelectMany(ts => ts.Players)
				.Select(ts => ts.Id).ToArray();
			var groups = championship.Stages.SelectMany(stage => stage.Groups)
				.Select(group => group.Id).ToArray();
			var vacancys = championship.Stages.SelectMany(stage => stage.Groups)
				.SelectMany(group => group.Vacancys).Select(vacancy => vacancy.Id).ToArray();

			invalids.AddRange(_context.Groups.Where(group => groups.Contains(group.Id))
				.Select(group => group.Id).ToArray());
			invalids.AddRange(_context.Vacancys.Where(vacancy => vacancys.Contains(vacancy.Id))
				.Select(vacancy => vacancy.Id).ToArray());
			invalids.AddRange(_context.PlayerSubscribe.Where(player => players.Contains(player.Id))
				.Select(player => player.Id).ToArray());
			invalids.AddRange(_context.TeamSubscribes.Where(team => teams.Contains(team.Id))
				.Select(team => team.Id).ToArray());
			return invalids.ToArray();
		}
		public Championship GetByIdWithStageGroupMatchs(string id)
		{
			return _context.Championships.Where(chp => chp.Id == id)
				.Include(chp => chp.Stages)
					.ThenInclude(stg => ((Stage)stg).Groups)
						.ThenInclude(grp => ((Group)grp).Matchs)
							.ThenInclude(mtc => ((Match)mtc).Home.Team)
				.Include(chp => chp.Stages)
					.ThenInclude(stg => ((Stage)stg).Groups)
						.ThenInclude(grp => ((Group)grp).Matchs)
							.ThenInclude(mtc => ((Match)mtc).Away.Team)
				.Include(chp => chp.Stages)
					.ThenInclude(stg => ((Stage)stg).Groups)
						.ThenInclude(grp => ((Group)grp).Matchs)
							.ThenInclude(mtc => ((Match)mtc).VacancyHome)
				.Include(chp => chp.Stages)
					.ThenInclude(stg => ((Stage)stg).Groups)
						.ThenInclude(grp => ((Group)grp).Matchs)
							.ThenInclude(mtc => ((Match)mtc).VacancyAway).FirstOrDefault();
		}
	}
}
