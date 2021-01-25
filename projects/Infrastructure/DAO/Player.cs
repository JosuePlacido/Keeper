using System.Linq;
using System.Threading.Tasks;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Keeper.Infrastructure.DAO;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Keeper.Application.DAO;
using Keeper.Domain.Utils;
using System.Collections.Generic;
using Keeper.Domain.Enum;

namespace Keeper.Infrastructure.DAO
{
	public class DAOPlayer : IDAOPlayer
	{
		private readonly ApplicationContext _context;
		public DAOPlayer(ApplicationContext Context)
		{
			_context = Context;
		}

		public async Task<PlayerViewDTO> GetByIdView(string id)
		{
			var player = await _context.Players.AsNoTracking()
				.Where(t => t.Id == id).FirstOrDefaultAsync();
			return new PlayerViewDTO
			{
				Id = player?.Id,
				Name = player?.Name,
				Nickname = player?.Nickname,
				IsDeletable = _context.PlayerSubscribe.Where(ts => ts.PlayerId == id).Count() == 0
			};
		}

		public async Task<PlayerSubscribe[]> GetFreeAgentsInChampionship(string championship)
		{
			string[] teamsId = await _context.TeamSubscribes.AsNoTracking()
				.Where(ts => ts.ChampionshipId == championship).Select(ts => ts.Id).ToArrayAsync();
			return await _context.PlayerSubscribe.AsNoTracking()
				.Where(ps => teamsId.Contains(ps.TeamSubscribeId))
				.Where(ps => ps.Status == Status.FreeAgent).ToArrayAsync();
		}

		public async Task<int> GetTotalFromSearch(string terms, string notInChampionship)
		{
			string termsNormalized = StringUtils.NormalizeLower(terms);
			string[] subscribed = await _context.Championships.Where(c => c.Id == notInChampionship)
				.SelectMany(c => c.Teams.SelectMany(ts => ts.Players)).Select(ps => ps.PlayerId).ToArrayAsync();
			return await _context.Players.AsNoTracking()
				.Where(p => !subscribed.Contains(p.Id))
				.Where(p => EF.Property<string>(p, "NormalizedName").Contains(termsNormalized)
					|| EF.Property<string>(p, "NormalizedNick").Contains(termsNormalized)).CountAsync();
		}
	}
}
