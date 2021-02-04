using System;
using System.Threading.Tasks;
using FluentValidation.Results;
using Keeper.Application.Models;
using Keeper.Application.DTO;
using Keeper.Domain.Models;

namespace Keeper.Application.Interface
{
	public interface IChampionshipService : IDisposable
	{
		Task<IServiceResult> Create(ChampionshipCreateDTO dto);
		MatchEditsScope CheckMatches(MatchEditsScope dto);
		Task<SquadEditDTO[]> GetSquads(string championship);
		Task<ObjectRenameDTO> GetNames(string championship);
		Task<IServiceResult> RenameScopes(ObjectRenameDTO dto);
		Task<IServiceResult> UpdateSquad(PLayerSquadPostDTO[] squads);
		Task<RankDTO> Rank(string championship);
		Task<IServiceResult> UpdateStatistics(RankPost[] dto);
		Task<TeamStatisticDTO[]> TeamStats(string id);
		Task<PlayerStatisticDTO[]> PlayerStats(string id);
		Task<IServiceResult> UpdateTeamsStatistics(TeamSubscribePost[] dto);
		Task<IServiceResult> UpdatePlayersStatistics(PlayerSubscribePost[] dto);
	}
}
