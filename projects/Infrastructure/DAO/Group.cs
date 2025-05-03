using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Contract.DAL;

namespace Infrastructure.DAO
{
	public class DAOGroup : DAO, IDAOGroup
	{
		public DAOGroup(ApplicationContext Context) : base(Context) { }

		public async Task<Group> GetByIdWithStatistics(string id)
		{
			return await _context.Groups.AsNoTracking().Where(g => g.Id == id)
				.Include(g => g.Statistics.OrderBy(s => s.Position))
				.FirstOrDefaultAsync();
		}

		public async Task<Group> GetByIdWithStatisticsAndTeamSubscribe(string group)
		{
			return await _context.Groups.AsNoTracking().Where(g => g.Id == group)
				.Include(g => g.Statistics)
					.ThenInclude(s => ((Statistic)s).TeamSubscribe)
				.FirstOrDefaultAsync();
		}

		public void Update(Group group)
		{
			_context.Entry(group).State = EntityState.Modified;
		}
	}
}
