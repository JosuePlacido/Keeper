using Keeper.Domain.Core;
using System;
using System.Collections.Generic;
namespace Keeper.Domain.Models
{
	public class Match : Entity
	{
		public string Name { get; private set; }
		public string Address { get; private set; }
		public DateTime? Date { get; private set; }
		public Vacancy VacancyHome { get; private set; }
		public string VacancyHomeId { get; private set; }
		public Vacancy VacancyAway { get; private set; }
		public string VacancyAwayId { get; private set; }
		public string HomeId { get; private set; }
		public TeamSubscribe Home { get; private set; }
		public string AwayId { get; private set; }
		public TeamSubscribe Away { get; private set; }
		public int Round { get; private set; }
		public int? GoalsHome { get; private set; }
		public int? GoalsAway { get; private set; }
		public int? GoalsPenaltyHome { get; private set; }
		public int? GoalsPenaltyAway { get; private set; }
		public bool FinalGame { get; private set; }
		public bool AggregateGame { get; private set; }
		public bool Penalty { get; private set; }
		public int? AggregateGoalsAway { get; private set; }
		public int? AggregateGoalsHome { get; private set; }
		public string Status { get; private set; }
		public string GroupId { get; private set; }
		public IList<EventGame> EventGames { get; set; }
		private Match() { }
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
			EventGames = new List<EventGame>();
		}

		public void EditScope(string name)
		{
			Name = name;
		}

		public void RegisterResult(int homeGoals, int awayGoals,
			int? homePenalties = null, int? awayPenalties = null, EventGame[] events = null)
		{
			Status = Enum.Status.Finish;
			GoalsHome = homeGoals;
			GoalsAway = awayGoals;
			if (AggregateGame)
			{
				AggregateGoalsHome += GoalsHome;
				AggregateGoalsAway += GoalsAway;
			}
			if (Penalty && FinalGame &&
				((AggregateGame && AggregateGoalsAway == AggregateGoalsHome) ||
				GoalsPenaltyHome == GoalsPenaltyAway))
			{
				GoalsPenaltyHome = homePenalties;
				GoalsPenaltyAway = awayPenalties;
			}
			if (events != null)
				foreach (var eg in events)
				{
					EventGames.Add(eg);
				}
		}
		public static Match Factory(string id, string name, string groupId, int round,
			string status = Enum.Status.Created,
			string vacancyHomeId = null, string vacancyAwayId = null,
			string homeId = null, string awayId = null,
			string address = null, DateTime? date = null,
			int? goalsHome = null, int? goalsAway = null,
			int? goalsPenaltyHome = null, int? goalsPenaltyAway = null,
			bool aggregateGame = false, bool penalty = false, bool finalGame = false,
			int? aggregateGoalsAway = null, int? aggregateGoalsHome = null,
			TeamSubscribe home = null, Vacancy vacancyHome = null,
			TeamSubscribe away = null, Vacancy vacancyAway = null, IList<EventGame> eventGames = null)
		{
			return new Match
			{
				Id = id,
				Name = name,
				Address = address,
				Date = date,
				VacancyHome = vacancyHome,
				VacancyHomeId = vacancyHomeId,
				VacancyAway = vacancyAway,
				VacancyAwayId = vacancyAwayId,
				HomeId = homeId,
				Home = home,
				AwayId = awayId,
				Away = away,
				Round = round,
				GoalsHome = goalsHome,
				GoalsAway = goalsAway,
				GoalsPenaltyHome = goalsPenaltyHome,
				GoalsPenaltyAway = goalsPenaltyAway,
				FinalGame = finalGame,
				AggregateGame = aggregateGame,
				Penalty = penalty,
				AggregateGoalsAway = aggregateGoalsAway,
				AggregateGoalsHome = aggregateGoalsHome,
				Status = status,
				GroupId = groupId,
				EventGames = eventGames,
			};
		}
	}
}
