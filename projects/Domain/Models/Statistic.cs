using Keeper.Domain.Core;
using Keeper.Domain.Enum;

namespace Keeper.Domain.Models
{
	public class Statistic : Entity
	{
		public string GroupId { get; private set; }
		public string TeamSubscribeId { get; private set; }
		public TeamSubscribe TeamSubscribe { get; private set; }
		public int Games { get; private set; }
		public int Won { get; private set; }
		public int Drowns { get; private set; }
		public int Lost { get; private set; }
		public int GoalsScores { get; private set; }
		public int Position { get; private set; }
		public int GoalsAgainst { get; private set; }
		public int GoalsDifference { get; private set; }
		public int Yellows { get; private set; }
		public int Reds { get; private set; }
		public int Points { get; private set; }
		public string Lastfive { get; private set; }
		public int RankMovement { get; private set; }
		private Statistic() { }
		public Statistic(string team)
		{
			TeamSubscribeId = team;
			Position = 1;
		}
		public Statistic Reorder(int newPosition)
		{
			if (Games > 1)
			{
				RankMovement = Position - newPosition;
			}
			Position = newPosition;
			return this;
		}
		public override string ToString()
		{
			return (TeamSubscribe == null) ? base.ToString() : $"{TeamSubscribe.Team.Name}";
		}

		public static Statistic Factory(string id, string teamSubscribeId, string groupId = null,
			 TeamSubscribe teamSubscribe = null, int games = 0, int won = 0, int drowns = 0, int lost = 0,
			 int goalsScores = 0, int position = 1, int goalsAgainst = 0,
			 int goalsDifference = 0, int yellows = 0, int reds = 0, int points = 0,
			 string lastfive = "", int rankMovement = 0)
		{
			return new Statistic()
			{
				Id = id,
				TeamSubscribeId = teamSubscribeId,
				TeamSubscribe = teamSubscribe,
				Games = games,
				Won = won,
				Drowns = drowns,
				Lost = lost,
				GoalsScores = goalsScores,
				Position = position,
				GoalsAgainst = goalsAgainst,
				GoalsDifference = goalsDifference,
				Yellows = yellows,
				Reds = reds,
				Points = points,
				Lastfive = lastfive,
				RankMovement = rankMovement,
			};
		}
	}
}
