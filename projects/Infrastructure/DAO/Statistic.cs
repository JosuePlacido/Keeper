using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Contract.DAL;

namespace Infrastructure.DAO
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
