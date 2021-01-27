using System.Threading.Tasks;
using AutoMapper;
using Keeper.Domain.Repository;
using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Keeper.Infrastructure.Repository
{
	public class ChampionshipRepository : RepositoryBase<Championship>, IRepositoryChampionship
	{
		public ChampionshipRepository(ApplicationContext Context) : base(Context) { }
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

		public async Task<Championship> GetByIdWithTeamsWithPLayers(string championship)
		{
			return await _context.Championships.AsNoTracking().Where(c => c.Id == championship)
				.Include(c => c.Teams)
					.ThenInclude(ts => ts.Team)
				.Include(c => c.Teams)
					.ThenInclude(ts => ts.Players)
						.ThenInclude(ps => ((PlayerSubscribe)ps).Player).FirstOrDefaultAsync();
		}

		public async Task<PlayerSubscribe> UpdatePLayer(PlayerSubscribe player)
		{
			PlayerSubscribe temp = await _context.PlayerSubscribe.FindAsync(player.Id);
			if (temp == null)
			{
				await _context.PlayerSubscribe.AddAsync(
					new PlayerSubscribe(player.PlayerId, player.TeamSubscribeId, player.Status));
			}
			else if (!await _context.PlayerSubscribe.AnyAsync(psi =>
				psi.Id == player.Id && psi.PlayerId == player.PlayerId &&
				psi.TeamSubscribeId == player.TeamSubscribeId && psi.Status == player.Status))
			{
				temp.TransferTeam(player.TeamSubscribeId, player.Status);
				_context.Entry(temp).State = EntityState.Modified;
			}
			return player;
		}
	}
}
