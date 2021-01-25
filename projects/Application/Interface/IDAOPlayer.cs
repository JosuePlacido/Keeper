using System.Collections.Generic;
using System.Threading.Tasks;
using Keeper.Application.DTO;
using Keeper.Domain.Models;

namespace Keeper.Application.DAO
{
	public interface IDAOPlayer
	{
		Task<PlayerViewDTO> GetByIdView(string id);
		Task<int> GetTotalFromSearch(string terms, string notInChampionship);
		Task<PlayerSubscribe[]> GetFreeAgentsInChampionship(string championship);
	}
}
