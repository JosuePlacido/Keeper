using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;
using Domain.Models;

namespace Application.Contract.DAL
{
	public interface IDAOStatistic : IDAO
	{
		Task<Statistic> GetById(string id);
		void UpdateAll(Statistic[] statistics);
	}
}
