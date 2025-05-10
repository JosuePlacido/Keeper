using Domain.Core;
using Domain.Enum;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Models
{
	public class Match : Entity, IAggregateRoot
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
		public IList<EventGame> EventGames { get; private set; }
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

		public void EditScope(int? round = null, string status = null, string name = null, string home = null, string away = null,
			string vacancyHome = null, string vacancyAway = null, DateTime? date = null,
			string address = "", bool? knockout = null, bool? finalGame = null, bool? penalty = null)
		{
			if (name != null)
			{
				Name = name;
			}
			if (round != null)
			{
				Round = (int)round;
			}
			if (!string.IsNullOrEmpty(status))
			{
				Status = status;
			}
			if (!string.IsNullOrEmpty(home))
				HomeId = home;

			if (!string.IsNullOrEmpty(away))
				AwayId = away;

			if (!string.IsNullOrEmpty(vacancyHome))
				VacancyHomeId = vacancyHome;

			if (!string.IsNullOrEmpty(vacancyAway))
				VacancyAwayId = vacancyAway;

			if (date != null)
				Date = date;

			if (!string.IsNullOrEmpty(address))
				Address = address;

			if (knockout != null)
				AggregateGame = (bool)knockout;

			if (finalGame != null)
				FinalGame = (bool)finalGame;

			if (penalty != null)
				Penalty = (bool)penalty;
		}

		public void UpdateAggregateResult(int goalsHome, int goalsAway)
		{
			AggregateGoalsHome = goalsHome;
			AggregateGoalsAway = goalsAway;
		}
		public void RegisterResult(int homeGoals, int awayGoals,
			int? homePenalties = null, int? awayPenalties = null, EventGame[] events = null)
		{
			UpdateTeamsStatistics(homeGoals, awayGoals);
			UpdatePlayerStatistics(events);
			RegisterEventGame(events);
			SetScore(homeGoals, awayGoals);
			SetAggregateScore();
			SetPenaltyScore(homePenalties, awayPenalties);
			TriggerDomainEvents();
		}

		private void TriggerDomainEvents()
		{
			AddDomainEvent(new PersistStatisticsMatchResultEvent(this));
			AddDomainEvent(new AdvanceChampionshipEvent(GroupId, Round));
		}

		private void SetPenaltyScore(int? homePenalties, int? awayPenalties)
		{
			if (Penalty && (AggregateGame && AggregateGoalsAway == AggregateGoalsHome || !AggregateGame && GoalsAway == GoalsHome))
			{
				GoalsPenaltyHome = homePenalties;
				GoalsPenaltyAway = awayPenalties;
			}
		}

		private void SetAggregateScore()
		{
			if (AggregateGame)
			{
				AggregateGoalsHome = GoalsHome + (AggregateGoalsHome ?? 0);
				AggregateGoalsAway = GoalsAway + (AggregateGoalsAway ?? 0);
			}
		}

		private void SetScore(int homeGoals, int awayGoals)
		{
			Status = Enum.Status.Finish;
			GoalsHome = homeGoals;
			GoalsAway = awayGoals;
		}

		private void UpdatePlayerStatistics(EventGame[] events)
		{
			if (events == null || events.Length == 0)
				return;

			if (Status == Enum.Status.Finish)
			{
				ApplyStats(EventGames.ToArray(), revert: true);
			}

			ApplyStats(events);

			int homeYellows = events.Count(e => e.IsHomeEvent && e.Type == TypeEvent.YellowCard);
			int homeReds = events.Count(e => e.IsHomeEvent && e.Type == TypeEvent.RedCard);
			int awayYellows = events.Count(e => !e.IsHomeEvent && e.Type == TypeEvent.YellowCard);
			int awayReds = events.Count(e => !e.IsHomeEvent && e.Type == TypeEvent.RedCard);

			if (Status == Enum.Status.Finish)
			{
				homeYellows -= EventGames.Count(e => e.IsHomeEvent && e.Type == TypeEvent.YellowCard);
				homeReds -= EventGames.Count(e => e.IsHomeEvent && e.Type == TypeEvent.RedCard);
				awayYellows -= EventGames.Count(e => !e.IsHomeEvent && e.Type == TypeEvent.YellowCard);
				awayReds -= EventGames.Count(e => !e.IsHomeEvent && e.Type == TypeEvent.RedCard);
			}

			Home.UpdateCards(homeYellows, homeReds);
			Away.UpdateCards(awayYellows, awayReds);
		}

		private void ApplyStats(EventGame[] eventGames, bool revert = false)
		{
			var players = eventGames.Select(ev => ev.RegisterPlayerId).Distinct();
			foreach (var playerId in players)
			{
				bool isHome = Home.Players.Any(p => p.Id == playerId);
				var playerEvents = eventGames.Where(ev => ev.RegisterPlayerId == playerId);

				int mult = revert ? -1 : 1;
				int goals = mult * playerEvents.Count(ev => ev.Type == TypeEvent.Goal);
				int yellows = mult * playerEvents.Count(ev => ev.Type == TypeEvent.YellowCard);
				int reds = mult * playerEvents.Count(ev => ev.Type == TypeEvent.RedCard);
				int mvps = mult * playerEvents.Count(ev => ev.Type == TypeEvent.MVP);

				var player = isHome ? Home.Players.First(p => p.Id == playerId)
									: Away.Players.First(p => p.Id == playerId);

				player.UpdateResult(goals, yellows, reds, mvps, Status == Enum.Status.Finish);
			}
		}

		private void UpdateTeamsStatistics(int homeGoals, int awayGoals)
		{
			if (Status == Enum.Status.Finish)
			{
				Home.UpdateResult(homeGoals - (int)GoalsHome,
					awayGoals - (int)GoalsAway,
					(int)GoalsHome - (int)GoalsAway,
					homeGoals - awayGoals);
				Away.UpdateResult(awayGoals - (int)GoalsAway,
					homeGoals - (int)GoalsHome,
					-(int)GoalsHome + (int)GoalsAway,
					-homeGoals + awayGoals
					);
			}
			else
			{
				Home.RegisterResult((int)homeGoals, (int)awayGoals);
				Away.RegisterResult((int)awayGoals, (int)homeGoals);
			}
		}

		private void RegisterEventGame(EventGame[] events = null)
		{
			EventGames = new List<EventGame>();
			if (events != null && events.Length > 0)
			{
				((List<EventGame>)EventGames).AddRange(events);
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
