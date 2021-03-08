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
	public class UpdateChampionshipDomainEventHandler : INotificationHandler<UpdateChampionshipEvent>
	{
		private readonly IUnitOfWork _uow;

		public UpdateChampionshipDomainEventHandler(IUnitOfWork uow)
		{
			_uow = uow;
		}
		public async Task Handle(UpdateChampionshipEvent notification, CancellationToken cancellationToken)
		{
			IDAOGroup daoGroup = (IDAOGroup)_uow.GetDAO(typeof(IDAOGroup));
			IDAOMatch daoMatch = (IDAOMatch)_uow.GetDAO(typeof(IDAOMatch));
			IDAOStage daoStage = (IDAOStage)_uow.GetDAO(typeof(IDAOStage));
			IRepositoryChampionship daoChampionship = (IRepositoryChampionship)_uow
				.GetDAO(typeof(IRepositoryChampionship));
			IDAOTeamSubscribe daoTeamSubscribe = (IDAOTeamSubscribe)_uow
				.GetDAO(typeof(IDAOTeamSubscribe));

			Group group = await daoGroup.GetByIdWithStatisticsAndTeamSubscribe(notification.Group);
			if (group.CurrentRound < notification.Round &&
				await daoMatch.HasPenndentMatchesWithDateInRound(group.Id, group.CurrentRound))
			{
				group.NextRound(notification.Round);
				daoGroup.Update(group);
			}
			if (!await daoMatch.IsOpenGroup(notification.Group))
			{
				int teamsPossibleClassifieds = group.SharedVacancyForNextStage +
					group.VacancyForNextStage;
				IList<Statistic> internalRanking = group.Statistics
					.OrderBy(s => s.Position).ToList();
				foreach (var team in internalRanking)
				{
					if (team.Position > teamsPossibleClassifieds)
					{
						team.TeamSubscribe.ChangeStatus(Status.Eliminated);
					}
				}
				if (teamsPossibleClassifieds == 0)
				{
					group.Statistics.Where(s => s.Position == 1).FirstOrDefault()
						.TeamSubscribe.ChangeStatus(Status.Champion);
				}
				daoTeamSubscribe.UpdateAll(group.Statistics.Select(s => s.TeamSubscribe).ToArray());

				Stage currentStage = await daoStage.GetById(group.StageId);
				if (!await daoStage.IsOpenStage(currentStage.Id, group.Id))
				{
					Stage nextStage = await daoStage.GetByChampionshipAndSequence(
						currentStage.ChampionshipId, currentStage.Order + 1);
					if (nextStage == null)
					{
						Championship championship = await daoChampionship
							.GetById(currentStage.ChampionshipId);
						championship.ChangeStatus(Status.Finish);
						await daoChampionship.Update(championship);
					}
					else if (nextStage.Regulation == Classifieds.Configured)
					{
						//TODO Se a Próxima Fase Tiver regulamento Pré definido já inscreve os times nos lugares
					}
					else if (nextStage.Regulation == Classifieds.Configured)
					{
						//TODO Se A Fase tiver Terminado e a próxima tiver regulamento melhores vs piores faz o
						//TODO ranking e inscreve os times nos devidos lugares
					}
					else
					{
						//TODO libera operação de Realizar Sorteio
					}
				}
			}
		}
	}
}
