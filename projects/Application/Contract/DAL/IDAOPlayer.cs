using System.Threading.Tasks;
using Application.DTO;
using Domain.Models;

namespace Application.Contract.DAL
{
	public interface IDAOPlayer : IDAO
	{
		Task<PlayerViewDTO> GetByIdView(string id);
		Task<int> GetTotalFromSearch(string terms, string notInChampionship);
		Task<PlayerSubscribe[]> GetFreeAgentsInChampionship(string championship);
		Task<string[]> Exists(string[] vs);
	}
}
