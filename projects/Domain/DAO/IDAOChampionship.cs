using System.Collections.Generic;
using System.Threading.Tasks;
using Keeper.Domain.Core;
using Keeper.Domain.Models;

namespace Keeper.Application.DAO
{
	public interface IDAOChampionship
	{
		Task<IDTO> GetByIdForRename(string id);
	}
}
