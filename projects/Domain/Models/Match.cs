using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class Match : Base
	{
		public virtual string Name { get; private set; }
		public virtual string Address { get; private set; }
		public virtual DateTime? Date { get; private set; }
		public virtual string GroupId { get; init; }
		public virtual Group Group { get; set; }
		public virtual Vacancy VacancyHome { get; set; }
		public virtual string VacancyHomeId { get; init; }
		public virtual Vacancy VacancyAway { get; set; }
		public virtual string VacancyAwayId { get; init; }
		public virtual string HomeId { get; init; }
		public virtual TeamSubscribe Home { get; private set; }
		public virtual string AwayId { get; init; }
		public virtual TeamSubscribe Away { get; private set; }
		public virtual int Round { get; init; }
		public virtual int? GoalsHome { get; private set; }
		public virtual int? GoalsAway { get; private set; }
		public virtual int? GoalsPenaltyHome { get; private set; }
		public virtual int? GoalsPenaltyAway { get; private set; }
		public virtual bool FinalGame { get; init; }
		public virtual bool AggregateGame { get; init; }
		public virtual bool Penalty { get; init; }
		public virtual int? AggregateGoalsAway { get; private set; }
		public virtual int? AggregateGoalsHome { get; private set; }
		public virtual string Status { get; private set; }
		public virtual IEnumerable<EventGame> EventGames { get; set; }
		public Match() { }
		public Match(int round, string status, string name, string home = null, string away = null,
			string vacancyHome = null, string vacancyAway = null, DateTime? date = null,
			string address = "", bool knockout = false, bool finalGame = false, bool penalty = false)
		{
			Round = round;
			Status = status;
			Name = name;
			HomeId = home;
			AwayId = away;
			VacancyHomeId = vacancyHome;
			VacancyAwayId = vacancyAway;
			Date = date;
			Address = address;
			AggregateGame = knockout;
			FinalGame = finalGame;
			Penalty = penalty;
			if (AggregateGame)
			{
				AggregateGoalsHome = AggregateGoalsAway = 0;
			}
		}

		public void EditMatchDetails(string name = null, string status = null, string address = null,
			 DateTime? date = null)
		{
			if (string.IsNullOrEmpty(name))
			{
				Name = name;
			}
			if (string.IsNullOrEmpty(status))
			{
				Status = status;
			}
			if (string.IsNullOrEmpty(address))
			{
				Address = address;
			}
			if (date != null)
			{
				Date = date;
			}
		}
		public override string ToString()
		{
			return Name;
		}

		public void RegisterResult(int homeGoals, int awayGoals,
			int? homeGoalsPenalty = null, int? awayGoalsPenalty = null)
		{
			GoalsHome = homeGoals;
			GoalsAway = awayGoals;
			GoalsPenaltyHome = homeGoalsPenalty;
			GoalsPenaltyAway = awayGoalsPenalty;
			if (AggregateGame)
			{
				AggregateGoalsHome += homeGoals;
				AggregateGoalsAway += awayGoals;
			}
		}
	}
}
