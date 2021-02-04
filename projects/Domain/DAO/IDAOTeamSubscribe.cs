using System.Threading.Tasks;
using Keeper.Domain.Core;
using Keeper.Domain.Models;

namespace Keeper.Application.DAO
{
	public interface IDAOTeamSubscribe : IDAO
	{
		Task<TeamSubscribe[]> GetByChampionshipTeamStatistics(string championship);
	}
}
