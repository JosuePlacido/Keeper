using System.Threading.Tasks;
using Keeper.Domain.Core;
using Keeper.Domain.Models;

namespace Keeper.Application.DAO
{
	public interface IDAOTeam
	{
		Task<IDTO> GetByIdView(string id);
		Task<int> GetTotalFromSearch(string terms, string championship);
	}
}
