
using System.Threading.Tasks;
using Keeper.Domain.Core;
using Keeper.Domain.Models;

namespace Keeper.Domain.Repository
{
	public interface IRepositoryMatch : IRepositoryBase<Match>
	{
		Task<Match> GetByIdWithTeamsAndPlayers(string id);
		Task<Match> RegisterResult(Match match);
		Task<Match[]> GetByGroupAndTeams(string group, string[] teams);
	}
}
