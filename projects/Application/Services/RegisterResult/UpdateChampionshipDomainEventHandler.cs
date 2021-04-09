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
			Group group = notification.Group;
			IDAOGroup daoGroup = (IDAOGroup)_uow.GetDAO(typeof(IDAOGroup));
			IDAOMatch daoMatch = (IDAOMatch)_uow.GetDAO(typeof(IDAOMatch));
			IDAOStage daoStage = (IDAOStage)_uow.GetDAO(typeof(IDAOStage));
			IDAOStatistic daoStatistic = (IDAOStatistic)_uow.GetDAO(typeof(IDAOStatistic));
			IRepositoryChampionship daoChampionship = (IRepositoryChampionship)_uow
				.GetDAO(typeof(IRepositoryChampionship));
			IDAOTeamSubscribe daoTeamSubscribe = (IDAOTeamSubscribe)_uow
				.GetDAO(typeof(IDAOTeamSubscribe));

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
			//* CHEGOU AQUI PQ O GRUPO TA FECHADO
			//TODO erro aqui ele faz a ligação SÒ quando a fase acaba

			Stage nextStage = await daoStage.GetByChampionshipAndSequence(
				currentStage.ChampionshipId, currentStage.Order + 1);

			if (nextStage == null)
			{
				Championship championship = await daoChampionship
					.GetById(currentStage.ChampionshipId);
				championship.ChangeStatus(Status.Finish);
				await daoChampionship.Update(championship);
			}
			else
			{
				if (nextStage.Regulation == Classifieds.Configured)
				{
					for (int x = 0; x < internalRanking.Count; x++)
					{
						Vacancy vacancy = nextStage.Groups.SelectMany(g => g.Vacancys.Where(v =>
							v.FromGroupId == group.Id &&
							v.FromPosition == internalRanking[x].Position &&
							v.FromStageOrder == currentStage.Order)).FirstOrDefault();
						group.AddTeam(internalRanking[x].TeamSubscribeId);
						if (vacancy != null)
						{
							await daoGroup.SetTeamOnVacancy(vacancy, internalRanking[x].TeamSubscribeId);
						}
					}
					await daoStatistic.UpdateAll(group.Statistics.ToArray());
					//TODO testar a montagem da proxima fase
					//TODO atualizar placares agregados
				}
				if (!await daoStage.IsOpenStage(currentStage.Id, group.Id))
				{
					if (nextStage.Regulation == Classifieds.Configured)
					{
						//TODO Se A Fase tiver Terminado e a próxima tiver regulamento melhores vs piores faz o
						//TODO ranking e inscreve os times nos devidos lugares
					}
					else
					{
						nextStage.AvailableForRandom();
					}
					daoStage.Update(nextStage);
				}
			}
		}
	}
}
