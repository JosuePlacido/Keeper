using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Provider;

namespace Domain.Rules.TiebreakCriterion
{
	public class ExtraMatchCriterion : BaseTiebreakCriterion
	{
		public ExtraMatchCriterion()
		{
			Name = "Jogo desempate";
			Id = 9;
		}

		public override IEnumerable<Statistic> Apply(IEnumerable<Statistic> stats,
			IMatchDataProvider matchProvider)
		{
			List<Statistic> statsTemp = stats.ToList();
			if (matchProvider.HasMatchsPendingInGroup(new string[] { statsTemp[0].GroupId }))
			{
				return stats;
			}
			//TODO implementar saporra
			return stats;
		}
	}

}
