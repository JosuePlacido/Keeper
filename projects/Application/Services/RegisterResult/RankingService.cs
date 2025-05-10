using System.Collections.Generic;
using System.Linq;
using Application.Contract.DAL;
using Application.Contract.Repository;
using Domain.Models;
using Domain.Provider;
using Domain.Rules.TiebreakCriterion;

namespace Application.Services.RegisterResult
{
	public interface IRankingService
	{
		Statistic[] Sort(IEnumerable<Statistic> stats, ITiebreakCriterion[] criteria);
	}
	public class RankingService : IRankingService
	{
		private readonly IUnitOfWork _uow;
		private readonly IMatchDataProvider _matchProvider;
		public RankingService(IUnitOfWork uow, IMatchDataProvider matchProvider)
		{
			_uow = uow;
			_matchProvider = matchProvider;
		}
		private bool HasDuplicatePositions(IEnumerable<Statistic> statistics)
		{
			return statistics
				.GroupBy(s => s.Position)
				.Any(g => g.Count() > 1);
		}


		public Statistic[] Sort(IEnumerable<Statistic> stats, ITiebreakCriterion[] criteria)
		{
			IEnumerable<Statistic> tempStats = stats.ToList();
			foreach (var criterion in criteria)
			{
				tempStats = criterion.Apply(tempStats, _matchProvider);
				if (HasDuplicatePositions(tempStats)) break;
			}
			var test = tempStats.Select(t => t.Position).ToArray();
			return tempStats.ToArray();
		}
	}
}
