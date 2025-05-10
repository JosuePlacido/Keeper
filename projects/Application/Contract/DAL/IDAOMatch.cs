using System.Threading.Tasks;

namespace Application.Contract.DAL
{
	public interface IDAOMatch : IDAO
	{
		Task<bool> IsOpenGroup(string group);
		Task<bool> HasPenndentMatchesWithDateInRound(string group, int currentRound);
	}
}
