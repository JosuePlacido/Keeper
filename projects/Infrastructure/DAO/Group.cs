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
	public class DAOGroup : DAO, IDAOGroup
	{
		public DAOGroup(ApplicationContext Context) : base(Context) { }

		public async Task<Group> GetByIdWithStatistics(string id)
		{
			return await _context.Groups.AsNoTracking().Where(g => g.Id == id)
				.Include(g => g.Statistics.OrderBy(s => s.Position))
				.FirstOrDefaultAsync();
		}
	}
}
