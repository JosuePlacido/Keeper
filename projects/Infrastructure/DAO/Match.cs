using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Contract.DAL;
using Domain.Enum;

namespace Infrastructure.DAO
{
	public class DAOMatch : DAO, IDAOMatch
	{
		public DAOMatch(ApplicationContext Context) : base(Context) { }

		public async Task<bool> HasPenndentMatchesWithDateInRound(string group, int currentRound)
		{
			return await _context.Matchs.AsNoTracking().AnyAsync(m => m.GroupId == group && m.Status
				!= Status.Finish && m.Date != null && m.Round == currentRound);
		}

		public async Task<bool> IsOpenGroup(string group)
		{
			return await _context.Matchs.AsNoTracking().AnyAsync(m => m.GroupId == group && m.Status
				!= Status.Finish);
		}
	}
}
