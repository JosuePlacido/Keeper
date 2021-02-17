using System.Threading.Tasks;
using Keeper.Domain.Models;

namespace Keeper.Application.Contract
{
	public interface IDAOStage : IDAO
	{
		Task<Stage> GetById(string id);
	}
}
