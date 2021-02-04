using Keeper.Domain.Repository;
using Keeper.Domain.Core;
using Keeper.Domain.Models;
using System.Threading.Tasks;

namespace Keeper.Domain.Repository
{
	public interface IRepositoryChampionship : IRepositoryBase<Championship>
	{
		Task<Championship> GetByIdWithTeamsWithPLayers(string championship);
		Task<Championship> GetByIdWithStageGroupsAndMatches(string championship);
		Task<PlayerSubscribe> UpdatePLayer(PlayerSubscribe player);
		Task<Championship> RenameScopes(Championship championship);
		Task<Championship> GetByIdWithRank(string championship);
		Task UpdateStatistics(Statistic[] statistics);
	}
}
