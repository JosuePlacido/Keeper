using System.Threading.Tasks;
using Keeper.Domain.Models;

namespace Keeper.Application.Contract
{
	public interface IDAOGroup : IDAO
	{
		Task<Group> GetByIdWithStatistics(string id);
		void Update(Group group);
		Task<Group> GetByIdWithStatisticsAndTeamSubscribe(string group);
		Task SetTeamOnVacancy(Vacancy vacancy, string teamSubscribeId);
		Task<bool> HasPenndentMatchesWithDateInRound(Group group);
		Task<bool> IsOpenGroup(string id);
	}
}
