using System;
using System.Reflection;
using System.Threading.Tasks;
using Keeper.Domain.Repository;
using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Keeper.Infrastructure.Repository
{
	public class PlayerRepository : RepositoryBase<Player>, IRepositoryPlayer
	{
		public PlayerRepository(ApplicationContext Context) : base(Context) { }

		public async Task<Player[]> GetAvailables(string terms, string championship, int page, int take)
		{
			string[] subscribed = await _context.Championships.AsNoTracking()
				.Where(c => c.Id == championship)
				.SelectMany(c => c.Teams.SelectMany(ts => ts.Players)).Select(ps => ps.PlayerId)
				.ToArrayAsync();
			return await _context.Players.AsNoTracking().Where(p => (p.Name.Contains(terms)
				|| p.Nickname.Contains(terms)) && !subscribed.Contains(p.Id))
				.OrderBy(t => t.Name).Skip((page - 1) * take)
				.Take(take).ToArrayAsync();
		}
	}
}
