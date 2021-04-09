using System.Linq;
using Domain.Models;
using Keeper.Domain.Core;
using Keeper.Domain.Enum;

namespace Keeper.Domain.Models
{
	public class Statistic : StatsManager
	{
		public string GroupId { get; private set; }
		public string TeamSubscribeId { get; private set; }
		public TeamSubscribe TeamSubscribe { get; private set; }
		public int Position { get; private set; }
		public int Points { get; private set; }
		public string Lastfive { get; private set; }
		public int RankMovement { get; private set; }
		public int LastPosition { get; private set; }
		private Statistic() { }
		public Statistic(string team, string group) : base()
		{
			TeamSubscribeId = team;
			Position = 1;
			LastPosition = 1;
			GroupId = group;
		}
		public Statistic RegisterResult(int goalsScore, int goalsAgainst)
		{
			LastPosition = Position;
			base.RegisterResult(goalsScore, goalsAgainst);

			if (goalsScore == goalsAgainst)
			{
				Lastfive += ",draw";
				Points++;
			}
			else if (goalsScore > goalsAgainst)
			{
				Lastfive += ",win";
				Points += 3;
			}
			else
			{
				Lastfive += ",lose";
			}

			string[] lastResults = Lastfive.Split(",");
			if (lastResults.Length > 5)
			{
				Lastfive = string.Join(",", lastResults.Skip(1).ToArray());
			}
			return this;
		}

		public Statistic UpdateResult(int goalsScoredDifference, int goalsAgainstDifference,
			int oldResult, int newResult, int roundMatch)
		{
			base.UpdateResult(goalsScoredDifference, goalsAgainstDifference, oldResult, newResult);
			string resultNewStatus = newResult == 0 ? "draw" : newResult > 0 ? "win" : "lose";
			string resultOldStatus = oldResult == 0 ? "draw" : oldResult > 0 ? "win" : "lose";
			if (resultNewStatus != resultOldStatus)
			{
				int pointDifference = (newResult > 0) ? 3 :
					(newResult == 0) ? 1 : 0;

				pointDifference -= (oldResult > 0) ? 3 :
					(oldResult == 0) ? 1 : 0;

				Points += pointDifference;

				string[] history = Lastfive.Split(",");
				if (Games - roundMatch < history.Length)
				{
					history[history.Length - (Games - roundMatch)] = resultNewStatus;
					Lastfive = string.Join(",", history);
				}
			}


			return this;
		}
		public Statistic UpdateNumbers(int? games = null, int? won = null, int? drowns = null,
			int? lost = null, int? goalsScores = null, int? goalsAgainst = null,
			int? goalsDifference = null, int? yellows = null, int? reds = null, int? points = null,
			int? position = null)
		{
			if (games != null)
				Games = (int)games;
			if (won != null)
				Won = (int)won;
			if (drowns != null)
				Drowns = (int)drowns;
			if (lost != null)
				Lost = (int)lost;
			if (goalsScores != null)
				GoalsScores = (int)goalsScores;
			if (position != null)
			{
				Reorder((int)position);
			}
			if (goalsAgainst != null)
				GoalsAgainst = (int)goalsAgainst;
			if (goalsDifference != null)
				GoalsDifference = (int)goalsDifference;
			if (yellows != null)
				Yellows = (int)yellows;
			if (reds != null)
				Reds = (int)reds;
			if (points != null)
				Points = (int)points;
			return this;
		}
		public Statistic Reorder(int newPosition)
		{
			if (Games > 1)
			{
				RankMovement = LastPosition - newPosition;
			}
			Position = newPosition;
			return this;
		}
		public string SetTeamOnVacancy(string teamId)
		{
			TeamSubscribeId = teamId;
			return Id;
		}
		public override string ToString()
		{
			return (TeamSubscribe == null) ? base.ToString() : $"{TeamSubscribe.Team.Name}";
		}
		public static Statistic Factory(string id, string teamSubscribeId, string groupId = null,
			 TeamSubscribe teamSubscribe = null, int games = 0, int won = 0, int drowns = 0, int lost = 0,
			 int goalsScores = 0, int position = 1, int goalsAgainst = 0,
			 int goalsDifference = 0, int yellows = 0, int reds = 0, int points = 0,
			 string lastfive = "", int LastPosition = 1, int rankMovement = 0)
		{
			return new Statistic()
			{
				Id = id,
				GroupId = groupId,
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
				LastPosition = LastPosition,
			};
		}
	}
}
