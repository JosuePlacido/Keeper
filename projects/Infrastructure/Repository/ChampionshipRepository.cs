using System.Threading.Tasks;
using AutoMapper;
using Keeper.Domain.Repository;
using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Keeper.Infrastructure.Repository
{
	public class ChampionshipRepository : RepositoryBase<Championship>, IRepositoryChampionship
	{
		public ChampionshipRepository(ApplicationContext Context) : base(Context) { }
		public override async Task<Championship> Add(Championship obj)
		{
			await base.Add(obj);
			await _context.Entry(obj).Collection(c => c.Stages).Query()
					.Include(stg => ((Stage)stg).Groups)
						.ThenInclude(grp => ((Group)grp).Matchs)
							.ThenInclude(mtc => ((Match)mtc).Home.Team)
				.Include(chp => chp.Groups)
						.ThenInclude(grp => ((Group)grp).Matchs)
							.ThenInclude(mtc => ((Match)mtc).Away.Team)
							.LoadAsync();
			return obj;
		}

	}
}
