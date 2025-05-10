
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Contract.Repository
{
	public interface IRepositoryMatch : IRepositoryBase<Match>
	{
		Task<Match> GetByIdWithTeamsAndPlayers(string id);
		Task<Match> RegisterResult(Match match);
		Task<Match[]> GetByGroupAndTeams(string group, string[] teams);
		Task<Match[]> GetAllMatchesPendingInGroups(string[] groupsId);
		Task<bool> HasPendentMatches(string id);
	}
}
