using System.Threading.Tasks;
using Keeper.Application.DTO;

namespace Keeper.Application.Contract
{
	public interface IDAOMatch : IDAO
	{
		Task<bool> IsOpenGroup(string group);
		Task<bool> HasPenndentMatchesWithDateInRound(string group, int currentRound);
	}
}
