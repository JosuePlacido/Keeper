using System.Threading.Tasks;
using Domain.Models;

namespace Application.Contract.DAL
{
	public interface IDAOStage : IDAO
	{
		Task<Stage> GetById(string id);
		Task<bool> IsOpenStage(string currentStage, string exceptGroup);
		Task<Stage> GetByChampionshipAndSequence(string championshipId, int sequence);
	}
}
