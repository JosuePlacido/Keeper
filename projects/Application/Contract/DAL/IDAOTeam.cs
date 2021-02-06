using System.Threading.Tasks;
using Keeper.Application.DTO;

namespace Keeper.Application.Contract
{
	public interface IDAOTeam : IDAO
	{
		Task<TeamViewDTO> GetByIdView(string id);
		Task<int> GetTotalFromSearch(string terms, string championship);
		Task<string[]> Exists(string[] ids);
	}
}
