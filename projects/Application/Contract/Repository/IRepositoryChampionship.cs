using Domain.Models;
using System.Threading.Tasks;

namespace Application.Contract.Repository
{
	public interface IRepositoryChampionship : IRepositoryBase<Championship>
	{
		Task<Championship> GetByIdWithTeamsWithPLayers(string championship);
		Task<Championship> GetByIdWithStageGroupsAndMatches(string championship);
		Task<PlayerSubscribe> UpdatePLayer(PlayerSubscribe player);
		Championship RenameScopes(Championship championship);
		Task<Championship> GetByIdWithRank(string championship);
		Task UpdateStatistics(Statistic[] statistics);
		Task<Championship> GetByIdWithMatchWithTeams(string id);
	}
}
