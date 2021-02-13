using System;
using System.Reflection;
using System.Threading.Tasks;
using Keeper.Domain.Repository;
using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Keeper.Domain.Utils;

namespace Keeper.Infrastructure.Repository
{
	public class MatchRepository : RepositoryBase<Match>, IRepositoryMatch
	{
		public MatchRepository(ApplicationContext Context) : base(Context) { }
		public async Task<Match> GetByIdWithTeamsAndPlayers(string id)
		{
			return await _context.Matchs.AsNoTracking().Where(m => m.Id == id)
				.Include(m => m.Home)
					.ThenInclude(ts => ts.Team)
				.Include(m => m.Home)
					.ThenInclude(ts => ts.Players)
						.ThenInclude(ps => ps.Player)
				.Include(m => m.Away)
					.ThenInclude(ts => ts.Team)
				.Include(m => m.Away)
					.ThenInclude(ts => ts.Players)
						.ThenInclude(ps => ps.Player)
				.Include(m => m.EventGames)
						.ThenInclude(ev => ev.RegisterPlayer)
							.ThenInclude(ps => ps.Player)
				.FirstOrDefaultAsync();
		}

		public async Task<Match> RegisterResult(Match match)
		{
			_context.EventGames.RemoveRange(_context.EventGames.Where(ev => match.Id == ev.MatchId));
			await _context.EventGames.AddRangeAsync(match.EventGames);
			_context.Entry(match).State = EntityState.Modified;
			_context.Entry(match.Home).State = EntityState.Modified;
			_context.Entry(match.Away).State = EntityState.Modified;
			foreach (var player in match.Home.Players)
			{
				_context.Entry(player).State = EntityState.Modified;
			}
			foreach (var player in match.Away.Players)
			{
				_context.Entry(player).State = EntityState.Modified;
			}
			return match;
		}
	}
}
