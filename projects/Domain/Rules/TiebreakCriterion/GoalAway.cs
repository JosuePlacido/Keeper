using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Provider;

namespace Domain.Rules.TiebreakCriterion
{
	public class GoalsAwayCriterion : BaseTiebreakCriterion
	{
		public GoalsAwayCriterion()
		{
			Name = "Gols como visitante";
			Id = 8;
		}

		public override IEnumerable<Statistic> Apply(IEnumerable<Statistic> stats,
			IMatchDataProvider matchProvider)
		{
			List<Statistic> statsTemp = stats.ToList();
			if (matchProvider.HasMatchsPendingInGroup(new string[] { statsTemp[0].GroupId }))
			{
				return stats;
			}
			var teamsDrowedsByRank = statsTemp
				.GroupBy(s => s.Position)
				.Where(g => g.Count() > 1);

			foreach (var rank in teamsDrowedsByRank)
			{
				Match[] matches = matchProvider.GetMatchsByGroupWithTeams(statsTemp[0].GroupId,
					rank.Select(s => s.TeamSubscribeId).ToArray());
				Dictionary<string, int> teamsDirectRank = rank.ToDictionary(s => s.TeamSubscribeId, s => 0);
				foreach (var match in matches)
				{
					teamsDirectRank[match.AwayId] += (int)match.GoalsAway;
				}
				string[] orderedTeamsId = teamsDirectRank
					.OrderByDescending(tdr => tdr.Value).Select(tdr => tdr.Key).ToArray();

				int position = rank.Key;
				int teamsDrowedCount = 1;
				Dictionary<string, Statistic> statsMap = statsTemp.ToDictionary(s => s.TeamSubscribeId);
				for (int x = 1; x < orderedTeamsId.Length; x++)
				{
					if (teamsDirectRank[orderedTeamsId[x]] == teamsDirectRank[orderedTeamsId[x - 1]])
					{
						teamsDrowedCount++;
					}
					else
					{
						position += teamsDrowedCount;
						teamsDrowedCount = 1;
					}
					statsMap[orderedTeamsId[x]].Reorder(position);
				}
			}

			return statsTemp.OrderBy(s => s.Position).ToList();
		}
	}

}
