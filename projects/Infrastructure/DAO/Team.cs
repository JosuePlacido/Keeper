using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Keeper.Infrastructure.DAO;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Keeper.Application.DAO;

namespace Keeper.Infrastructure.DAO
{
	public class DAOTeam : IDAOTeam
	{
		private readonly ApplicationContext _context;
		public DAOTeam(ApplicationContext Context)
		{
			_context = Context;
		}

		public async Task<TeamViewDTO> GetByIdView(string id)
		{
			var team = await _context.Teams.AsNoTracking()
				.Where(t => t.Id == id).FirstOrDefaultAsync();
			return new TeamViewDTO
			{
				Id = team?.Id,
				Name = team?.Name,
				Abrev = team?.Abrev,
				LogoUrl = team?.LogoUrl,
				IsDeletable = _context.TeamSubscribes.Where(ts => ts.TeamId == id).Count() == 0
			};
		}
	}
}
