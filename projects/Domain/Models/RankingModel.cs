using Keeper.Domain.Models;

namespace Domain.Models
{
	public class SortTeamModelHelper
	{
		public string TeamSubscribeId { get; set; }
		public int Games { get; set; }
		public int Won { get; set; }
		public int Drowns { get; set; }
		public int Lost { get; set; }
		public int GoalsScores { get; set; }
		public int Position { get; set; }
		public int GoalsAgainst { get; set; }
		public int GoalsDifference { get; set; }
		public int Points { get; set; }

		public void RegisterResult(int goals, int goalsAgainst)
		{
			int result = goals - goalsAgainst;
			GoalsScores += goals;
			GoalsAgainst += goalsAgainst;
			GoalsDifference += result;
			if (result == 0)
			{
				Points++;
				Drowns++;
			}
			else if (result > 0)
			{
				Points += 3;
				Won++;
			}
			else
			{
				Lost++;
			}
		}

		public SortTeamModelHelper(Statistic statistic)
		{
			TeamSubscribeId = statistic.TeamSubscribeId;
			Position = statistic.Position;
		}
	}
}
