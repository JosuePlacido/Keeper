using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Keeper.Infrastructure.DAO;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Keeper.Application.DAO;
using System.Linq.Expressions;
using System;
using Keeper.Domain.Utils;
using Keeper.Domain.Core;
using System.Collections.Generic;

namespace Keeper.Infrastructure.DAO
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
				_context.Entry(item).State = EntityState.Modified;
			}
		}
	}
}
