using System;
using System.Collections.Generic;
using Keeper.Domain.Core;

namespace Keeper.Domain.Models
{
	public class TeamSubscribe : Entity
	{
		public string ChampionshipId { get; private set; }
		public string Status { get; private set; }
		public string TeamId { get; private set; }
		public Team Team { get; private set; }
		public int Games { get; private set; }
		public int Won { get; private set; }
		public int Drowns { get; private set; }
		public int Lost { get; private set; }
		public int GoalsScores { get; private set; }
		public int GoalsAgainst { get; private set; }
		public int GoalsDifference { get; private set; }
		public int Yellows { get; private set; }
		public int Reds { get; private set; }
		public IList<PlayerSubscribe> Players { get; private set; }
		private TeamSubscribe() { }

		public TeamSubscribe UpdateNumbers(string status, int games, int won,
			int drowns, int lost, int goalsScores,
			int goalsAgainst, int goalsDifference, int yellows, int reds)
		{
			Status = status;
			Games = games;
			Won = won;
			Drowns = drowns;
			Lost = lost;
			GoalsScores = goalsScores;
			GoalsAgainst = goalsAgainst;
			GoalsDifference = goalsDifference;
			Yellows = yellows;
			Reds = reds;
			return this;
		}
		public TeamSubscribe AddPlayer(PlayerSubscribe player)
		{
			Players.Add(player);
			return this;
		}
		public TeamSubscribe AddPlayers(PlayerSubscribe[] players)
		{
			foreach (var player in players)
			{
				AddPlayer(player);
			}
			return this;
		}
		public TeamSubscribe RegisterResult(int goalsScore, int goalsAgainst)
		{
			Games++;
			goalsScore += goalsScore;
			GoalsAgainst += goalsAgainst;
			int goalsDifference = goalsScore - goalsAgainst;
			if (goalsDifference == 0)
			{
				Drowns++;
			}
			else
			{
				if (goalsDifference > 0)
					Won++;
				else
					Lost++;
				GoalsDifference += goalsDifference;
			}
			return this;
		}
		public TeamSubscribe(string team) : base(Guid.NewGuid().ToString())
		{
			Status = Enum.Status.Matching;
			TeamId = team;
			Players = new List<PlayerSubscribe>();
		}
	}
}
