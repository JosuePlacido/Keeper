using System.Threading.Tasks;
using Keeper.Domain.Models;

namespace Keeper.Application.Contract
{
	public interface IDAOGroup : IDAO
	{
		Task<Group> GetByIdWithStatistics(string id);
	}
}
