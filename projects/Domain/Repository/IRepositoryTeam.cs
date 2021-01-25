
using System.Threading.Tasks;
using Keeper.Domain.Core;
using Keeper.Domain.Models;

namespace Keeper.Domain.Repository
{
	public interface IRepositoryTeam : IRepositoryBase<Team>
	{
		Task<Team[]> GetAllAvailableForChampionship(string terms, string notInChampinship, int page, int take);
	}
}
