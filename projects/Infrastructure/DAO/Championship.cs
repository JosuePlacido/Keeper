using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Contract.DAL;
using Application.Services.EditChampionship;

namespace Infrastructure.DAO
{
	public class DAOChampionship : DAO, IDAOChampionship
	{
		public DAOChampionship(ApplicationContext Context) : base(Context)
		{
		}
		public async Task<ObjectRenameDTO> GetByIdForRename(string id)
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
