using System.Collections.Generic;
using System.Threading.Tasks;
using Keeper.Domain.Core;
using Keeper.Domain.Models;

namespace Keeper.Application.DAO
{
	public interface IDAOTeam : IDAO
	{
		Task<IDTO> GetByIdView(string id);
		Task<int> GetTotalFromSearch(string terms, string championship);
		Task<string[]> Exists(string[] ids);
	}
}
