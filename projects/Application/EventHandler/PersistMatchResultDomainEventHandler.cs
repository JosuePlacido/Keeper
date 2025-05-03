using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contract.DAL;
using Application.Contract.Repository;
using Domain.Enum;
using Domain.Events;
using Domain.Models;
using MediatR;

namespace Application.EventHandler
{
	public class PersistMatchResultDomainEventHandler : INotificationHandler<PersistStatisticsMatchResultEvent>
	{
		private readonly IUnitOfWork _uow;

		public PersistMatchResultDomainEventHandler(IUnitOfWork uow)
		{
			_uow = uow;
		}
		private static (int yellows, int reds) CountCards(IList<EventGame> events, bool isHome)
		{
			return (
				yellows: events.Count(e => e.IsHomeEvent == isHome && e.Type == TypeEvent.YellowCard),
				reds: events.Count(e => e.IsHomeEvent == isHome && e.Type == TypeEvent.RedCard)
			);
		}

		public async Task Handle(PersistStatisticsMatchResultEvent notification, CancellationToken cancellationToken)
		{
			var matchAnalyse = notification.Match;
			var repoMatch = (IRepositoryMatch)_uow.GetDAO(typeof(IRepositoryMatch));
			Match oldMatchState = await repoMatch.GetByIdWithTeamsAndPlayers(matchAnalyse.Id);

			var dao = (IDAOGroup)_uow.GetDAO(typeof(IDAOGroup));
			Group group = await dao.GetByIdWithStatistics(matchAnalyse.GroupId);

			int goalsHome = (int)matchAnalyse.GoalsHome;
			int goalsAway = (int)matchAnalyse.GoalsAway;
			var (yellowsHome, redsHome) = CountCards(matchAnalyse.EventGames, true);
			var (yellowsAway, redsAway) = CountCards(matchAnalyse.EventGames, true);

			if (oldMatchState.Status == Status.Finish)
			{
				goalsHome -= (int)oldMatchState.GoalsHome;
				goalsAway -= (int)oldMatchState.GoalsAway;

				var (oldYellowsHome, oldRedsHome) = CountCards(matchAnalyse.EventGames, true);
				var (oldYellowsAway, oldRedsAway) = CountCards(matchAnalyse.EventGames, true);

				yellowsHome -= oldYellowsHome;
				redsHome -= oldRedsHome;
				yellowsAway -= oldYellowsAway;
				redsAway -= oldRedsAway;

				UpdateTeamStatistic(group, matchAnalyse.HomeId, goalsHome, goalsAway,
					(int)oldMatchState.GoalsHome - (int)oldMatchState.GoalsAway,
					(int)matchAnalyse.GoalsHome - (int)matchAnalyse.GoalsAway,
					yellowsHome, redsHome, matchAnalyse.Round);
				UpdateTeamStatistic(group, matchAnalyse.AwayId, goalsAway, goalsHome,
					-(int)oldMatchState.GoalsHome + (int)oldMatchState.GoalsAway,
					-(int)matchAnalyse.GoalsHome + (int)matchAnalyse.GoalsAway,
					yellowsAway, redsAway, matchAnalyse.Round);
			}
			else
			{
				RegisterTeamResult(group, matchAnalyse.HomeId, goalsHome, goalsAway, yellowsHome, redsHome);
				RegisterTeamResult(group, matchAnalyse.AwayId, goalsAway, goalsHome, yellowsAway, redsAway);

			}

			Stage stage = await ((IDAOStage)_uow.GetDAO(typeof(IDAOStage))).GetById(group.StageId);

			group.UpdateRank(stage.Criterias);
		}

		private static void RegisterTeamResult(Group group, string teamId,
			int goals, int goalsAgainst, int yellows, int reds)
		{
			group.Statistics.First(s => s.TeamSubscribeId == teamId)
				.RegisterResult(goals, goalsAgainst)
				.UpdateCards(yellows, reds);
		}

		private static void UpdateTeamStatistic(Group group, string teamId, int goalsScoredGap,
			int goalsAgainstGap, int oldResult, int newResult, int yellowCardsGap, int redCardsGap,
			int round)
		{
			group.Statistics.First(s => s.TeamSubscribeId == teamId)
				 .UpdateResult(goalsScoredGap, goalsAgainstGap, oldResult, newResult, round)
				.UpdateCards(yellowCardsGap, redCardsGap);
		}
	}
}

