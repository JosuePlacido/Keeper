using System.Threading.Tasks;
using Keeper.Domain.Core;
using Keeper.Domain.Models;

namespace Keeper.Application.DAO
{
	public interface IDAOPlayerSubscribe : IDAO
	{
		Task<string> ValidateUpdateOnSquad(PlayerSubscribe player);
		Task<PlayerSubscribe[]> GetByChampionshipPlayerStatistics(string championship);
	}
}
