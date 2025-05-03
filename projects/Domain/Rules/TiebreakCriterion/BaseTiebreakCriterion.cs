using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Provider;

namespace Domain.Rules.TiebreakCriterion
{
	public abstract class BaseTiebreakCriterion : ITiebreakCriterion
	{
		public string Name { get; protected set; }
		public int Id { get; protected set; }

		protected IEnumerable<Statistic> ApplyCriterionReordering(
			IEnumerable<Statistic> stats,
			Func<Statistic, IComparable> keySelector,
			bool ascending = false)
		{
			var orderedStats = ascending
				? stats.OrderBy(s => s.Position).ThenBy(keySelector).ToArray()
				: stats.OrderBy(s => s.Position).ThenByDescending(keySelector).ToArray();

			Statistic prev = orderedStats[0];
			for (int x = 1; x < orderedStats.Length; x++)
			{
				Statistic current = orderedStats[x];
				if (current.Position <= prev.Position) //Caso de empate multiplos, o anterior vai ter posicao maior
				{
					current.Reorder(prev.Position + (keySelector(current) != keySelector(prev) ? 1 : 0));
				}
				prev = current;
			}
			return stats.OrderBy(s => s.Position).ToList();
		}
		public abstract IEnumerable<Statistic> Apply(IEnumerable<Statistic> stats, IMatchDataProvider matchProvider = null);

	}
}
