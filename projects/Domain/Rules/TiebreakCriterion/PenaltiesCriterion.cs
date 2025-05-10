using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Provider;

namespace Domain.Rules.TiebreakCriterion
{
	public class PenaltiesCriterion : BaseTiebreakCriterion
	{
		public PenaltiesCriterion()
		{
			Name = "Penaltis";
			Id = 10;
		}

		public override IEnumerable<Statistic> Apply(IEnumerable<Statistic> stats,
			IMatchDataProvider matchProvider)
		{
			List<Statistic> teams = stats.ToList();
			if (matchProvider.HasMatchsPendingInGroup(new string[] { teams[0].GroupId }) && teams[0].Position == teams[1].Position)
			{
				Match[] matches = matchProvider.GetMatchsByGroupWithTeams(teams[0].GroupId,
					teams.Select(t => t.TeamSubscribeId).ToArray());
				Match matchWithPenalties = matches.First(m => m.FinalGame && m.Penalty);
				int result = (int)matchWithPenalties.GoalsPenaltyHome - (int)matchWithPenalties
					.GoalsPenaltyAway;
				Statistic team;
				if (result > 0)
				{
					team = teams.Where(t => t.TeamSubscribeId == matchWithPenalties.AwayId)
						.FirstOrDefault();
				}
				else
				{
					team = teams.Where(t => t.TeamSubscribeId == matchWithPenalties.HomeId)
						.FirstOrDefault();
				}
				team.Reorder(team.Position + 1);
			}
			return teams.OrderBy(t => t.Position).ToArray();
		}
	}
}
