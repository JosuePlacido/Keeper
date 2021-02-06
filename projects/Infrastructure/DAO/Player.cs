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
	public class DAOPlayer : DAO, IDAOPlayer
	{
		public DAOPlayer(ApplicationContext Context) : base(Context) { }

		public async Task<string[]> Exists(string[] ids)
		{
			List<string> idNotFound = new List<string>();
			Player player;
			foreach (var id in ids)
			{
				player = await _context.Players.AsNoTracking()
					.Where(t => t.Id == id).FirstOrDefaultAsync();
				if (player == null)
				{
					idNotFound.Add(id);
				}
			}
			return idNotFound.ToArray();
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
			string[] playersId = await _context.TeamSubscribes.AsNoTracking()
				.Where(ts => ts.ChampionshipId == championship).Select(ts => ts.Id).ToArrayAsync();
			return await _context.PlayerSubscribe.AsNoTracking()
				.Where(ps => playersId.Contains(ps.TeamSubscribeId))
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
