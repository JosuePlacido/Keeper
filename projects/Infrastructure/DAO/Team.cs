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

namespace Keeper.Infrastructure.DAO
{
	public class DAOTeam : IDAOTeam
	{
		private readonly ApplicationContext _context;
		public DAOTeam(ApplicationContext Context)
		{
			_context = Context;
		}

		public async Task<IDTO> GetByIdView(string id)
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

		public async Task<int> GetTotalFromSearch(string terms, string notInChampionship)
		{
			string termsNormalized = StringUtils.NormalizeLower(terms);
			string termsNormalizedUpper = termsNormalized.ToUpper();
			string[] subscribed = await _context.TeamSubscribes.AsNoTracking()
				.Where(ts => ts.ChampionshipId == notInChampionship).Select(ts => ts.TeamId)
				.ToArrayAsync();
			return await _context.Teams
				.Where(t => !subscribed.Contains(t.Id))
				.Where(t => EF.Property<string>(t, "NormalizedName").Contains(termsNormalized) ||
					t.Abrev.Contains(termsNormalizedUpper)).CountAsync();
		}
	}
}
