using Domain.Models;

namespace Domain.Provider
{
	public interface IMatchDataProvider
	{
		Match[] GetMatchsByGroupWithTeams(string Id, string[] teams);
		bool HasMatchsPendingInGroup(string[] groupsId);
	}
}
