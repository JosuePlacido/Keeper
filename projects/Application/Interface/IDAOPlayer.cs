using System.Threading.Tasks;
using Keeper.Application.DTO;

namespace Keeper.Application.DAO
{
	public interface IDAOPlayer
	{
		Task<PlayerViewDTO> GetByIdView(string id);
		Task<int> GetTotalFromSearch(string terms, string notInChampionship);
	}
}
