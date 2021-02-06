using System;
using System.Threading.Tasks;
using Keeper.Application.Contract;

namespace Keeper.Application.Services.EditChampionship
{
	public interface IEditChampionshipService : IDisposable
	{
		Task<SquadEditDTO[]> GetSquads(string championship);
		Task<ObjectRenameDTO> GetNames(string championship);
		Task<IServiceResponse> RenameScopes(ObjectRenameDTO dto);
		Task<IServiceResponse> UpdateSquad(PLayerSquadPostDTO[] squads);
		Task<RankDTO> Rank(string championship);
		Task<IServiceResponse> UpdateStatistics(RankPost[] dto);
		Task<TeamStatisticDTO[]> TeamStats(string id);
		Task<PlayerStatisticDTO[]> PlayerStats(string id);
		Task<IServiceResponse> UpdateTeamsStatistics(TeamSubscribePost[] dto);
		Task<IServiceResponse> UpdatePlayersStatistics(PlayerSubscribePost[] dto);
	}
}
