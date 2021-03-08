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
