using System.Linq;
using System.Threading.Tasks;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Keeper.Infrastructure.DAO;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Keeper.Application.DAO;
using Keeper.Domain.Utils;
using System.Collections.Generic;
using Keeper.Domain.Enum;
using Keeper.Domain.Core;

namespace Keeper.Infrastructure.DAO
{
	public class DAOChampionship : DAO, IDAOChampionship
	{
		public DAOChampionship(ApplicationContext Context) : base(Context)
		{
		}
		public async Task<IDTO> GetByIdForRename(string id)
		{
			return await _context.Championships.AsNoTracking().Where(c => c.Id == id)
				.Include(c => c.Stages)
					.ThenInclude(s => ((Stage)s).Groups)
						.ThenInclude(g => ((Group)g).Matchs)
				.Select(c => new ObjectRenameDTO
				{
					Id = c.Id,
					Name = c.Name,
					Childs = c.Stages.Select(s => new ObjectRenameDTO
					{
						Id = s.Id,
						Name = s.Name,
						Childs = s.Groups.Select(g => new ObjectRenameDTO
						{
							Id = g.Id,
							Name = g.Name,
							Childs = g.Matchs.Select(m => new ObjectRenameDTO
							{
								Id = m.Id,
								Name = m.Name
							}).ToArray()
						}).ToArray()
					}).ToArray()
				}).FirstOrDefaultAsync();
		}
	}
}
