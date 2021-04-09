using System.Threading.Tasks;
using Keeper.Domain.Models;

namespace Keeper.Application.Contract
{
	public interface IDAOStage : IDAO
	{
		Task<Stage> GetById(string id);
		Task<bool> IsOpenStage(string currentStage, string exceptGroup);
		Task<Stage> GetByChampionshipAndSequence(string championshipId, int sequence);
		void Update(Stage nextStage);
	}
}
