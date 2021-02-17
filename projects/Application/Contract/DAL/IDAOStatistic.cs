using System.Collections.Generic;
using System.Threading.Tasks;
using Keeper.Domain.Core;
using Keeper.Domain.Models;

namespace Keeper.Application.Contract
{
	public interface IDAOStatistic : IDAO
	{
		Task<Statistic> GetById(string id);
		void UpdateAll(Statistic[] statistics);
	}
}
