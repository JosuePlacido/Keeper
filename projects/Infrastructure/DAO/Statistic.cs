using System.Linq;
using System.Threading.Tasks;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Keeper.Infrastructure.DAO;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Keeper.Application.Contract;
using Keeper.Domain.Utils;
using System.Collections.Generic;
using Keeper.Domain.Enum;
using Keeper.Domain.Core;

namespace Keeper.Infrastructure.DAO
{
	public class DAOStatistic : DAO, IDAOStatistic
	{
		public DAOStatistic(ApplicationContext Context) : base(Context) { }

		public async Task<Statistic> GetById(string id)
		{
			return await _context.Statistics.AsNoTracking()
				.Where(s => s.Id == id).FirstOrDefaultAsync();
		}

		public void UpdateAll(Statistic[] statistics)
		{
			foreach (var stat in statistics)
			{
				_context.Entry(stat).State = EntityState.Modified;
			}
		}
	}
}
