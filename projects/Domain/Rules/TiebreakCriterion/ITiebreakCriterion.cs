using System.Collections.Generic;
using Domain.Models;
using Domain.Provider;

namespace Domain.Rules.TiebreakCriterion
{
	public interface ITiebreakCriterion
	{
		string Name { get; }
		int Id { get; }
		IEnumerable<Statistic> Apply(IEnumerable<Statistic> stats, IMatchDataProvider matchprovider = null);
	}
}
