using System.Threading.Tasks;
using Application.Services.CRUDTeam;

namespace Application.Contract.DAL
{
	public interface IDAOTeam : IDAO
	{
		Task<TeamViewDTO> GetByIdView(string id);
		Task<int> GetTotalFromSearch(string terms, string championship);
		Task<string[]> Exists(string[] ids);
	}
}
