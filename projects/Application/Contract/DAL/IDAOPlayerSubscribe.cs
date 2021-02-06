using System.Threading.Tasks;
using Keeper.Domain.Core;
using Keeper.Domain.Models;

namespace Keeper.Application.Contract
{
	public interface IDAOPlayerSubscribe : IDAO
	{
		Task<string> ValidateUpdateOnSquad(PlayerSubscribe player);
		Task<PlayerSubscribe[]> GetByChampionshipPlayerStatistics(string championship);
		Task<PlayerSubscribe[]> GetAllById(string[] vs);
		void UpdateAll(PlayerSubscribe[] list);
	}
}
