using System;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain.Utils;
using Application.Contract.Repository;

namespace Infrastructure.Repository
{
	public class PlayerRepository : RepositoryBase<Player>, IRepositoryPlayer
	{
		public PlayerRepository(ApplicationContext Context) : base(Context) { }

		public async Task<Player[]> GetAvailables(string terms, string championship, int page, int take)
		{
			string termsNormalized = StringUtils.NormalizeLower(terms);

			string[] subscribed = await _context.Championships.AsNoTracking()
				.Where(c => c.Id == championship)
				.SelectMany(c => c.Teams.SelectMany(ts => ts.Players)).Select(ps => ps.PlayerId)
				.ToArrayAsync();

			return await _context.Players.AsNoTracking()
				.Where(p => !subscribed.Contains(p.Id))
				.Where(p => EF.Property<string>(p, "NormalizedName").Contains(termsNormalized)
					|| EF.Property<string>(p, "NormalizedNick").Contains(termsNormalized))
				.OrderBy(t => t.Name).Skip((page - 1) * take)
				.Take(take).ToArrayAsync();
		}
	}
}
