using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Provider;

namespace Domain.Rules.TiebreakCriterion
{
	public class DirectMatchCriterion : BaseTiebreakCriterion
	{
		public DirectMatchCriterion()
		{
			Name = "Confronto direto";
			Id = 7;
		}

		public override IEnumerable<Statistic> Apply(IEnumerable<Statistic> stats,
			IMatchDataProvider matchProvider)
		{
			var teamsDrowedsByRank = stats
				.GroupBy(s => s.Position)
				.Where(g => g.Count() > 1);

			foreach (var rank in teamsDrowedsByRank)
			{
				List<Statistic> rankingOnlyDirectMatch = rank.
					Select(t => Statistic.Factory(t.Id, t.TeamSubscribeId, groupId: t.GroupId)).ToList();
				Dictionary<string, Statistic> teamStatistics = rankingOnlyDirectMatch
					.ToDictionary(r => r.TeamSubscribeId);

				Match[] matches = matchProvider.GetMatchsByGroupWithTeams(
					rankingOnlyDirectMatch[0].GroupId,
					rankingOnlyDirectMatch.Select(s => s.TeamSubscribeId).ToArray());

				if (matches.Length > 0)
				{
					foreach (var match in matches)
					{
						teamStatistics[match.HomeId]
							.RegisterResult((int)match.GoalsHome, (int)match.GoalsAway);

						teamStatistics[match.HomeId].RegisterResult(
								(int)match.GoalsAway, (int)match.GoalsHome);
					}

					int currentPosition = rankingOnlyDirectMatch[0].Position;
					int teamsDrowed = 1;

					rankingOnlyDirectMatch = rankingOnlyDirectMatch
						.OrderByDescending(s => s.Points)
						.ThenByDescending(s => s.GoalsDifference)
						.ThenByDescending(s => s.GoalsScores)
						.ThenBy(s => s.Games)
						.Select((s, index) =>
							{
								if (index > 0)
								{
									Statistic prevStats = rankingOnlyDirectMatch[index - 1];
									var current = Tuple.Create(s.Points, s.Won, s.GoalsScores, s.GoalsDifference, s.Games);
									var previous = Tuple.Create(prevStats.Points, prevStats.Won, prevStats.GoalsScores, prevStats.GoalsDifference, prevStats.Games);

									if (current.Equals(previous))
									{
										teamsDrowed++;
									}
									else
									{
										currentPosition += teamsDrowed;
										teamsDrowed = 1;
									}
									s.Reorder(currentPosition);
								}
								return s;
							}
						).ToList();

					foreach (var newRank in rankingOnlyDirectMatch)
					{
						stats.First(t => t.Position == rank.Key &&
							t.TeamSubscribeId == newRank.TeamSubscribeId)
							.Reorder(newRank.Position);
					}

				}
			}

			return stats.OrderBy(s => s.Position).ToArray();
		}
	}

}
