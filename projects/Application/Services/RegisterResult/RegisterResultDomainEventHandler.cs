using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Keeper.Application.Contract;
using Keeper.Domain.Enum;
using Keeper.Domain.Events;
using Keeper.Domain.Models;
using Keeper.Domain.Repository;
using MediatR;

namespace Keeper.Application.Services.RegisterResult
{
	public class RegisterResultDomainEventHandler : INotificationHandler<RegisterResultEvent>
	{
		private readonly IUnitOfWork _uow;

		public RegisterResultDomainEventHandler(IUnitOfWork uow)
		{
			_uow = uow;
		}
		public async Task Handle(RegisterResultEvent notification, CancellationToken cancellationToken)
		{
			var matchAnalyse = notification.Match;
			var repoMatch = ((IRepositoryMatch)_uow.GetDAO(typeof(IRepositoryMatch)));
			Match oldMatchState = await repoMatch.GetByIdWithTeamsAndPlayers(matchAnalyse.Id);

			var dao = (IDAOGroup)_uow.GetDAO(typeof(IDAOGroup));
			Group group = await dao.GetByIdWithStatistics(matchAnalyse.GroupId);

			int goalsHome = (int)matchAnalyse.GoalsHome;
			int goalsAway = (int)matchAnalyse.GoalsAway;
			int yellowsHome = matchAnalyse.EventGames.Where(ev => ev.IsHomeEvent && ev.Type
					== TypeEvent.YellowCard).Count();
			int yellowsAway = matchAnalyse.EventGames.Where(ev => !ev.IsHomeEvent && ev.Type
					== TypeEvent.YellowCard).Count();
			int redsHome = matchAnalyse.EventGames.Where(ev => ev.IsHomeEvent && ev.Type
					== TypeEvent.RedCard).Count();
			int redsAway = matchAnalyse.EventGames.Where(ev => !ev.IsHomeEvent && ev.Type
					== TypeEvent.RedCard).Count();

			if (oldMatchState.Status == Status.Finish)
			{
				goalsHome -= (int)oldMatchState.GoalsHome;
				goalsAway -= (int)oldMatchState.GoalsAway;
				yellowsHome -= oldMatchState.EventGames.Where(ev => ev.IsHomeEvent && ev.Type
						== TypeEvent.YellowCard).Count();
				yellowsAway -= oldMatchState.EventGames.Where(ev => !ev.IsHomeEvent && ev.Type
						== TypeEvent.YellowCard).Count();
				redsHome -= oldMatchState.EventGames.Where(ev => ev.IsHomeEvent && ev.Type
						== TypeEvent.RedCard).Count();
				redsAway -= oldMatchState.EventGames.Where(ev => !ev.IsHomeEvent && ev.Type
						== TypeEvent.RedCard).Count();

				group.Statistics.Where(s => s.TeamSubscribeId == matchAnalyse.HomeId)
					.FirstOrDefault().UpdateResult(goalsHome, goalsAway,
						(int)oldMatchState.GoalsHome - (int)oldMatchState.GoalsAway,
						(int)matchAnalyse.GoalsHome - (int)matchAnalyse.GoalsAway,
						matchAnalyse.Round)
					.UpdateCards(yellowsHome, redsHome);
				group.Statistics.Where(s => s.TeamSubscribeId == matchAnalyse.AwayId)
					.FirstOrDefault().UpdateResult(goalsAway, goalsHome,
						-(int)oldMatchState.GoalsHome + (int)oldMatchState.GoalsAway,
						-(int)matchAnalyse.GoalsHome + (int)matchAnalyse.GoalsAway,
						matchAnalyse.Round)
					.UpdateCards(yellowsAway, redsAway);
			}
			else
			{
				group.Statistics.Where(s => s.TeamSubscribeId == matchAnalyse.HomeId)
					.FirstOrDefault().RegisterResult(goalsHome, goalsAway)
					.UpdateCards(yellowsHome, redsHome);
				group.Statistics.Where(s => s.TeamSubscribeId == matchAnalyse.AwayId)
					.FirstOrDefault().RegisterResult(goalsAway, goalsHome)
					.UpdateCards(yellowsAway, redsAway);

			}

			Stage stage = await ((IDAOStage)_uow.GetDAO(typeof(IDAOStage))).GetById(group.StageId);

			TiebreackCriterion[] criterionList = stage.Criterias.Split(",").Select(i =>
				TiebreackCriterion.FromValue<TiebreackCriterion>(Convert.ToInt16(i))).ToArray();

			group.UpdateRank(criterionList, (group, teams) =>
			{
				return Task.Run(async () =>
				{
					Match[] result = await repoMatch.GetByGroupAndTeams(group, teams);
					if (matchAnalyse.GroupId == group && teams.Contains(matchAnalyse.HomeId)
						&& teams.Contains(matchAnalyse.HomeId))
					{
						result[Array.IndexOf(result, matchAnalyse)] = matchAnalyse;
					}
					return result;
				}).Result;
			});
			(((IDAOStatistic)_uow.GetDAO(typeof(IDAOStatistic))))
				.UpdateAll(group.Statistics.ToArray());
		}
	}
}
