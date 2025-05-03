using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Provider;

namespace Domain.Rules.TiebreakCriterion
{
	public class RandomCriterion : BaseTiebreakCriterion
	{
		public RandomCriterion()
		{
			Name = "Penaltis";
			Id = 10;
		}

		public override IEnumerable<Statistic> Apply(IEnumerable<Statistic> stats,
			IMatchDataProvider matchProvider)
		{
			List<Statistic> statsList = stats.ToList();
			if (matchProvider.HasMatchsPendingInGroup(new string[] { statsList[0].GroupId }))
			{
				return statsList.ToArray();
			}
			var teamsDrowedsByRank = statsList
				.GroupBy(s => s.Position)
				.Where(g => g.Count() > 1);

			foreach (var teamsDrowedInThisRank in teamsDrowedsByRank)
			{
				var teamsShuffled = teamsDrowedInThisRank.OrderBy(_ => Guid.NewGuid()).ToList();
				for (int x = 0; x < teamsShuffled.Count; x++)
				{
					teamsShuffled[x].Reorder(teamsDrowedInThisRank.Key + x);
				}
			}
			return statsList.OrderBy(t => t.Position).ToArray();
		}
	}
}
