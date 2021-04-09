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

namespace Keeper.Infrastructure.DAO
{
	public class DAOStage : DAO, IDAOStage
	{
		public DAOStage(ApplicationContext Context) : base(Context) { }

		public async Task<Stage> GetByChampionshipAndSequence(string championshipId, int sequence)
		{
			return await _context.Stages.AsNoTracking().Where(s => s.ChampionshipId == championshipId)
				.Where(s => s.Order == sequence)
				.Include(s => s.Groups)
					.ThenInclude(g => ((Group)g).Vacancys)
				.Include(s => s.Groups)
					.ThenInclude(g => ((Group)g).Statistics)
				.FirstOrDefaultAsync();
		}

		public async Task<Stage> GetById(string id)
		{
			return await _context.Stages.AsNoTracking().Where(s => s.Id == id)
				.FirstOrDefaultAsync();
		}

		public async Task<bool> IsOpenStage(string currentStage, string exceptGroup)
		{
			string[] groups = await _context.Groups.AsNoTracking()
				.Where(g => g.StageId == currentStage && g.Id != exceptGroup)
				.Select(g => g.Id).ToArrayAsync();

			return await _context.Matchs.AsNoTracking().AnyAsync(m => groups.Contains(m.GroupId)
				&& m.Status != Status.Finish);
		}

		public void Update(Stage nextStage)
		{
			_context.Entry(nextStage).State = EntityState.Modified;
		}
	}
}
