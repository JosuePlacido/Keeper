using System.Threading.Tasks;
using Keeper.Application.DTO;
using Keeper.Domain.Models;

namespace Keeper.Application.Contract
{
	public interface IDAOPlayer : IDAO
	{
		Task<PlayerViewDTO> GetByIdView(string id);
		Task<int> GetTotalFromSearch(string terms, string notInChampionship);
		Task<PlayerSubscribe[]> GetFreeAgentsInChampionship(string championship);
		Task<string[]> Exists(string[] vs);
	}
}
