using System.Threading.Tasks;
using Domain.Models;

namespace Application.Contract.DAL
{
	public interface IDAOGroup : IDAO
	{
		Task<Group> GetByIdWithStatistics(string id);
		void Update(Group group);
		Task<Group> GetByIdWithStatisticsAndTeamSubscribe(string group);
	}
}
