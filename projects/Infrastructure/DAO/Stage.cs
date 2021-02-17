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
	public class DAOStage : DAO, IDAOStage
	{
		public DAOStage(ApplicationContext Context) : base(Context) { }

		public async Task<Stage> GetById(string id)
		{
			return await _context.Stages.AsNoTracking().Where(s => s.Id == id)
				.FirstOrDefaultAsync();
		}
	}
}
