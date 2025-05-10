using System.Collections.Generic;
using Domain.Models;
using Domain.Provider;

namespace Domain.Rules.TiebreakCriterion
{
	public class VictoryCriterion : BaseTiebreakCriterion
	{
		public VictoryCriterion()
		{
			Name = "Vitórias";
			Id = 1;
		}

		public override IEnumerable<Statistic> Apply(IEnumerable<Statistic> stats,
			IMatchDataProvider matchProvider = null) => ApplyCriterionReordering(stats, x => x.Won);
	}
	public class GoalsDifferenceCriterion : BaseTiebreakCriterion
	{
		public GoalsDifferenceCriterion()
		{
			Name = "Saldo de gols";
			Id = 2;
		}

		public override IEnumerable<Statistic> Apply(IEnumerable<Statistic> stats,
			IMatchDataProvider matchProvider = null) => ApplyCriterionReordering(stats, x => x.GoalsDifference);
	}
	public class GoalsScoredCriterion : BaseTiebreakCriterion
	{
		public GoalsScoredCriterion()
		{
			Name = "Gols marcados";
			Id = 3;
		}

		public override IEnumerable<Statistic> Apply(IEnumerable<Statistic> stats,
			IMatchDataProvider matchProvider = null) => ApplyCriterionReordering(stats, x => x.GoalsScores);
	}
	public class YellowCardCriterion : BaseTiebreakCriterion
	{
		public YellowCardCriterion()
		{
			Name = "Cartões amarelos";
			Id = 4;
		}

		public override IEnumerable<Statistic> Apply(IEnumerable<Statistic> stats,
			IMatchDataProvider matchProvider = null) => ApplyCriterionReordering(stats, x => x.Yellows, ascending: true);
	}

	public class RedCardCriterion : BaseTiebreakCriterion
	{
		public RedCardCriterion()
		{
			Name = "Cartões vermelhos";
			Id = 5;
		}

		public override IEnumerable<Statistic> Apply(IEnumerable<Statistic> stats,
			IMatchDataProvider matchProvider = null) => ApplyCriterionReordering(stats, x => x.Reds, ascending: true);
	}
	public class GoalsAgainstCriterion : BaseTiebreakCriterion
	{
		public GoalsAgainstCriterion()
		{
			Name = "Gols sofridos";
			Id = 6;
		}

		public override IEnumerable<Statistic> Apply(IEnumerable<Statistic> stats,
			IMatchDataProvider matchProvider = null) => ApplyCriterionReordering(stats, x => x.GoalsAgainst, ascending: true);
	}
}
