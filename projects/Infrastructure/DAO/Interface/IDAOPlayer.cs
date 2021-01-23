using System.Threading.Tasks;
using Keeper.Domain.Models;
using Keeper.Infrastructure.CrossCutting.DTO;

namespace Keeper.Infrastructure.DAO
{
	public interface IDAOPlayer : IDAO<Player>
	{
		Task<PlayerViewDTO> GetByIdView(string id);
		Task<int> GetTotalFromSearch(string terms, string notInChampionship);
	}
}
