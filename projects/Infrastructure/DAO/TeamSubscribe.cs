using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Application.Contract.DAL;
using System;

namespace Infrastructure.DAO
{
	public class DAOTeamSubscribe : DAO, IDAOTeamSubscribe
	{
		public DAOTeamSubscribe(ApplicationContext Context) : base(Context) { }

		public async Task<TeamSubscribe[]> GetAllById(string[] ids)
		{
			return await _context.TeamSubscribes.AsNoTracking()
				.Where(ts => ids.Contains(ts.Id)).ToArrayAsync();
		}

		public async Task<TeamSubscribe[]> GetByChampionshipTeamStatistics(string championship)
		{
			return await _context.TeamSubscribes.AsNoTracking()
				.Where(ts => ts.ChampionshipId == championship)
					.Include(ts => ts.Team).ToArrayAsync();
		}

		public void UpdateAll(TeamSubscribe[] list)
		{
			foreach (var item in list)
			{
				if (_context.Entry(item).State != EntityState.Modified)
					_context.Entry(item).State = EntityState.Modified;
			}
		}
	}
}
