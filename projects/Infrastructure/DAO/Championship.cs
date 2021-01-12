using Infrastructure.Data;
using Domain.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.DAO
{
	public class DAOChampionship : DAOBase<Championship>
	{
		private readonly ApplicationContext _context;
		public DAOChampionship(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}

		public override Championship Add(Championship obj)
		{
			base.Add(obj);
			_context.Entry(obj).Collection(c => c.Stages).Query()
					.Include(stg => ((Stage)stg).Groups)
						.ThenInclude(grp => ((Group)grp).Matchs)
							.ThenInclude(mtc => ((Match)mtc).Home.Team)
				.Include(chp => chp.Groups)
						.ThenInclude(grp => ((Group)grp).Matchs)
							.ThenInclude(mtc => ((Match)mtc).Away.Team)
							.Load();
			return obj;
		}

		internal Championship GetByIdWithStageGroupMatchs(string id)
		{
			return _context.Championships.Where(chp => chp.Id == id)
				.Include(chp => chp.Stages)
					.ThenInclude(stg => ((Stage)stg).Groups)
						.ThenInclude(grp => ((Group)grp).Matchs)
							.ThenInclude(mtc => ((Match)mtc).Home.Team)
				.Include(chp => chp.Stages)
					.ThenInclude(stg => ((Stage)stg).Groups)
						.ThenInclude(grp => ((Group)grp).Matchs)
							.ThenInclude(mtc => ((Match)mtc).Away.Team)
				.Include(chp => chp.Stages)
					.ThenInclude(stg => ((Stage)stg).Groups)
						.ThenInclude(grp => ((Group)grp).Matchs)
							.ThenInclude(mtc => ((Match)mtc).VacancyHome)
				.Include(chp => chp.Stages)
					.ThenInclude(stg => ((Stage)stg).Groups)
						.ThenInclude(grp => ((Group)grp).Matchs)
							.ThenInclude(mtc => ((Match)mtc).VacancyAway).FirstOrDefault();
		}
	}
}
