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
	public class DAOPlayerSubscribe : IDAOPlayerSubscribe
	{
		private readonly ApplicationContext _context;
		public DAOPlayerSubscribe(ApplicationContext Context)
		{
			_context = Context;
		}
		public async Task<string> ValidateUpdateOnSquad(PlayerSubscribe player)
		{
			bool PlayerExists = await _context.Players.AsNoTracking()
				.AnyAsync(p => p.Id == player.PlayerId);
			if (!PlayerExists)
				return "Jogador não registrado";
			var temp = _context.TeamSubscribes.AsNoTracking().ToArray();
			bool TeamExsit = await _context.TeamSubscribes.AsNoTracking()
					.AnyAsync(ts => ts.Id == player.TeamSubscribeId);
			if (!TeamExsit)
				return "Inscrição do Time não registrada";
			else
			{
				bool IsNewSubscribe = !await _context.PlayerSubscribe.AsNoTracking()
					.AnyAsync(ps => ps.Id == player.Id);
				TeamSubscribe team = await _context.TeamSubscribes.AsNoTracking()
					.Where(ts => ts.Id == player.TeamSubscribeId).FirstOrDefaultAsync();
				if (IsNewSubscribe)
				{
					if (await _context.TeamSubscribes.AsNoTracking()
					.AnyAsync(ts => ts.ChampionshipId == team.ChampionshipId &&
						ts.Players.Select(p => p.PlayerId).ToArray().Contains(player.PlayerId)))
					{
						return "Jogador já está inscrito no campeonato";
					};
				}
				else
				{
					bool TransferInSameChampionship = await _context.TeamSubscribes.AsNoTracking()
						.AnyAsync(ts => ts.Id == player.TeamSubscribeId &&
						ts.ChampionshipId == team.ChampionshipId);
					if (!TransferInSameChampionship)
						return "As inscrições dos times não pertencem ao mesmo campeonato";
					bool PlayerSubscribeIsLegit = await _context.PlayerSubscribe.AsNoTracking()
						.AnyAsync(ps => ps.Id == player.Id && ps.PlayerId == player.PlayerId);
					if (!PlayerSubscribeIsLegit)
						return "Inscrição pertence a outro jogador";
				}
			}
			return null;
		}
	}
}
