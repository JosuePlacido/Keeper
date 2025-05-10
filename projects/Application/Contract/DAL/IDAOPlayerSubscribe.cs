using System.Threading.Tasks;
using Domain.Core;
using Domain.Models;

namespace Application.Contract.DAL
{
	public interface IDAOPlayerSubscribe : IDAO
	{
		Task<string> ValidateUpdateOnSquad(PlayerSubscribe player);
		Task<PlayerSubscribe[]> GetByChampionshipPlayerStatistics(string championship);
		Task<PlayerSubscribe[]> GetAllById(string[] vs);
		void UpdateAll(PlayerSubscribe[] list);
	}
}
