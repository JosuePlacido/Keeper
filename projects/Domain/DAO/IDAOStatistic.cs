using System.Collections.Generic;
using System.Threading.Tasks;
using Keeper.Domain.Core;
using Keeper.Domain.Models;

namespace Keeper.Application.DAO
{
	public interface IDAOStatistic
	{
		Task<Statistic> GetById(string id);
	}
}
