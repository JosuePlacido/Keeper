
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Contract.Repository
{
	public interface IRepositoryTeam : IRepositoryBase<Team>
	{
		Task<Team[]> GetAllAvailableForChampionship(string terms, string notInChampinship, int page, int take);
	}
}
