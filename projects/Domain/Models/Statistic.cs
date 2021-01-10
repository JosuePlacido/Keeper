using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;
using Domain.Enum;

namespace Domain.Models
{
	public class Statistic : Entity
	{
		public string GroupId { get; private set; }
		public Group Group { get; private set; }
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
		public RankMovement RankMovement { get; private set; }
		private Statistic() { }
		public Statistic(string team)
		{
			TeamSubscribeId = team;
			Position = 1;
		}

		public Statistic UpdateNumbers(int games, int won, int drowns,
			int lost, int goalsScores, int position, int goalsAgainst,
			int goalsDifference, int yellows, int reds, int points,
			string lastfive, RankMovement rankMovement)
		{
			Games = games;
			Won = won;
			Drowns = drowns;
			Lost = lost;
			GoalsScores = goalsScores;
			Position = position;
			GoalsAgainst = goalsAgainst;
			GoalsDifference = goalsDifference;
			Yellows = yellows;
			Reds = reds;
			Points = points;
			Lastfive = lastfive;
			RankMovement = rankMovement;
			return this;
		}
		public override string ToString()
		{
			return (TeamSubscribe == null) ? base.ToString() : $"{TeamSubscribe.Team.Name}";
		}
	}
}
