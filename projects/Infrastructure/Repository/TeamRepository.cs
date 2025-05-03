using System;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain.Utils;
using Application.Contract.Repository;

namespace Infrastructure.Repository
{
	public class TeamRepository : RepositoryBase<Team>, IRepositoryTeam
	{
		public TeamRepository(ApplicationContext Context) : base(Context) { }
		public async Task<Team[]> GetAllAvailableForChampionship(string terms, string notInChampinship,
			 int page, int take)
		{
			string termsNormalized = StringUtils.NormalizeLower(terms);
			string termsNormalizedUpper = termsNormalized.ToUpper();


			string[] subscribed = await _context.TeamSubscribes.AsNoTracking()
				.Where(ts => ts.ChampionshipId == notInChampinship)
				.Select(ts => ts.TeamId)
				.ToArrayAsync();

			return await _context.Teams.AsNoTracking()
				.Where(t => !subscribed.Contains(t.Id))
				.Where(t => EF.Property<string>(t, "NormalizedName").Contains(termsNormalized) ||
					t.Abrev.Contains(termsNormalizedUpper))
				.OrderBy(t => t.Name)
				.Skip((page - 1) * take).Take(take).ToArrayAsync();
		}
	}
}
