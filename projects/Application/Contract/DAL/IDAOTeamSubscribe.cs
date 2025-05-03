using System.Threading.Tasks;
using Domain.Models;

namespace Application.Contract.DAL
{
	public interface IDAOTeamSubscribe : IDAO
	{
		Task<TeamSubscribe[]> GetByChampionshipTeamStatistics(string championship);
		Task<TeamSubscribe[]> GetAllById(string[] ids);
		void UpdateAll(TeamSubscribe[] list);
	}
}
