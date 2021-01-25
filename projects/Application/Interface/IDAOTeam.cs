using System.Threading.Tasks;
using Keeper.Domain.Models;
using Keeper.Application.DTO;

namespace Keeper.Application.DAO
{
	public interface IDAOTeam
	{
		Task<TeamViewDTO> GetByIdView(string id);
		Task<int> GetTotalFromSearch(string terms, string championship);
	}
}
