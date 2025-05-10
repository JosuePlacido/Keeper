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
	public class AdvanceChampionshipDomainEventHandler : INotificationHandler<AdvanceChampionshipEvent>
	{
		private readonly IUnitOfWork _uow;

		public AdvanceChampionshipDomainEventHandler(IUnitOfWork uow)
		{
			_uow = uow;
		}
		public async Task Handle(AdvanceChampionshipEvent notification, CancellationToken cancellationToken)
		{
			IDAOGroup daoGroup = (IDAOGroup)_uow.GetDAO(typeof(IDAOGroup));
			IDAOMatch daoMatch = (IDAOMatch)_uow.GetDAO(typeof(IDAOMatch));

			Group group = await daoGroup.GetByIdWithStatisticsAndTeamSubscribe(notification.Group);

			await TryToAdvanceRoundAsync(group, notification.Round, daoMatch, daoGroup);

			if (!await daoMatch.IsOpenGroup(notification.Group))
			{
				SetEliminatedAndChampionTeams(group);
				await HandleStageProgression(group);
			}
		}

		private async Task HandleStageProgression(Group group)
		{
			IRepositoryChampionship daoChampionship = (IRepositoryChampionship)_uow
				.GetDAO(typeof(IRepositoryChampionship));
			IDAOStage daoStage = (IDAOStage)_uow.GetDAO(typeof(IDAOStage));
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
					return;
				}
				switch (nextStage.Regulation)
				{
					case Classifieds.Configured:
						//TODO Se a Próxima Fase Tiver regulamento Pré definido já inscreve os times nos lugares
						break;
					case Classifieds.BestVsWorst:
						//TODO Se A Fase tiver Terminado e a próxima tiver regulamento melhores vs piores faz o
						//TODO ranking e inscreve os times nos devidos lugares
						break;
					default: //TODO libera operação de Realizar Sorteio
						break;
				}
			}
		}

		private void SetEliminatedAndChampionTeams(Group group)
		{
			IDAOTeamSubscribe daoTeamSubscribe = (IDAOTeamSubscribe)_uow
				.GetDAO(typeof(IDAOTeamSubscribe));
			int nextStageVacancies = group.SharedVacancyForNextStage +
				group.VacancyForNextStage;
			Statistic[] internalRanking = group.Statistics
				.OrderBy(s => s.Position).ToArray();
			foreach (var team in internalRanking)
			{
				if (team.Position > nextStageVacancies)
				{
					team.TeamSubscribe.ChangeStatus(Status.Eliminated);
				}
			}
			if (nextStageVacancies == 0)
			{
				group.Statistics.Where(s => s.Position == 1).FirstOrDefault()
					.TeamSubscribe.ChangeStatus(Status.Champion);
			}
			daoTeamSubscribe.UpdateAll(group.Statistics.Select(s => s.TeamSubscribe).ToArray());
		}

		private static async Task TryToAdvanceRoundAsync(Group group, int round,
			IDAOMatch daoMatch, IDAOGroup daoGroup)
		{
			if (group.CurrentRound < round &&
				!await daoMatch.HasPenndentMatchesWithDateInRound(group.Id, group.CurrentRound))
			{
				group.NextRound();
				daoGroup.Update(group);
			}
		}
	}
}
