using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Provider;

namespace Domain.Rules.TiebreakCriterion
{

	public class PointsCriterion : BaseTiebreakCriterion
	{
		public PointsCriterion()
		{
			Name = "Pontos";
			Id = 0;
		}

		public override IEnumerable<Statistic> Apply(IEnumerable<Statistic> stats,
			IMatchDataProvider matchProvider = null)
		{
			List<Statistic> statsOrdered = stats.OrderBy(s => s.Points).ToList();
			if (statsOrdered[0].Position != 1)
			{
				statsOrdered[0].Reorder(1);
			}

			int position = 1;
			int teamsDrowed = 1;
			bool drowed = true;

			for (int x = 1; x < statsOrdered.Count; x++)
			{
				drowed = statsOrdered[x].Points == statsOrdered[x - 1].Points;
				if (drowed)
				{
					teamsDrowed++;
				}
				else
				{
					position += teamsDrowed;
					teamsDrowed = 1;
				}
				statsOrdered[x].Reorder(position);
			}
			return stats;
		}
	}

}
