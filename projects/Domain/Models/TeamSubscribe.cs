using System;
using System.Collections.Generic;
using Keeper.Domain.Core;

namespace Keeper.Domain.Models
{
	public class TeamSubscribe : StatsManager
	{
		public string ChampionshipId { get; private set; }
		public string Status { get; private set; }
		public string TeamId { get; private set; }
		public Team Team { get; private set; }
		public IList<PlayerSubscribe> Players { get; private set; }
		private TeamSubscribe() { }
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
		public TeamSubscribe(string team) : base(Guid.NewGuid().ToString())
		{
			Status = Enum.Status.Matching;
			TeamId = team;
			Players = new List<PlayerSubscribe>();
		}
		public static TeamSubscribe Factory(string id, string teamId,
			string championshipId = null, string status = Enum.Status.Matching, Team team = null,
			int games = 0, int won = 0, int drowns = 0, int lost = 0, int goalsScores = 0,
			int goalsAgainst = 0, int goalsDifference = 0, int yellows = 0, int reds = 0,
			IList<PlayerSubscribe> players = null)
		{
			return new TeamSubscribe
			{
				Id = id,
				Status = status,
				TeamId = teamId,
				Team = team,
				Games = games,
				Won = won,
				Drowns = drowns,
				Lost = lost,
				GoalsScores = goalsScores,
				GoalsAgainst = goalsAgainst,
				GoalsDifference = goalsDifference,
				Yellows = yellows,
				Reds = reds,
				Players = players,
				ChampionshipId = championshipId
			};
		}

		public void ChangeStatus(string status)
		{
			Status = status;
		}

		public void UpdateNumbers(int? games = null, int? drowns = null, int? goalsAgainst = null,
			int? goalsDifference = null, int? goalsScores = null, int? lost = null,
			int? reds = null, int? won = null, int? yellows = null)
		{
			if (games != null)
				Games = (int)games;
			if (drowns != null)
				Drowns = (int)drowns;
			if (goalsAgainst != null)
				GoalsAgainst = (int)goalsAgainst;
			if (goalsDifference != null)
				GoalsDifference = (int)goalsDifference;
			if (goalsScores != null)
				GoalsScores = (int)goalsScores;
			if (lost != null)
				Lost = (int)lost;
			if (reds != null)
				Reds = (int)reds;
			if (won != null)
				Won = (int)won;
			if (yellows != null)
				Yellows = (int)yellows;
		}
	}
}
