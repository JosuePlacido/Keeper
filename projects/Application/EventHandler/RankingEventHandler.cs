using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contract.DAL;
using Application.Services.RegisterResult;
using Domain.Events;
using Domain.Rules.TiebreakCriterion;
using MediatR;

namespace Application.EventHandler
{
	public class RankingEventHandler : INotificationHandler<RankingEvent>
	{
		private readonly IUnitOfWork _uow;
		private readonly IRankingService _rankingService;
		private readonly Dictionary<int, ITiebreakCriterion> _criterionMap;

		public RankingEventHandler(IUnitOfWork uow, IEnumerable<ITiebreakCriterion> allCriteria, IRankingService rankingService)
		{
			_uow = uow;
			_criterionMap = allCriteria.ToDictionary(c => c.Id);
			_rankingService = rankingService;
		}

		public async Task Handle(RankingEvent notification, CancellationToken cancellationToken)
		{
			ITiebreakCriterion[] criterionList = notification.CriteriaIds.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(id =>
			{
				if (!_criterionMap.TryGetValue(Convert.ToInt16(id), out var criterion))
					throw new InvalidOperationException($"Critério de desempate '{id}' não encontrado.");
				return criterion;
			}).ToArray();

			var statsUpdated = _rankingService.Sort(notification.Group.Statistics, criterionList);

			((IDAOStatistic)_uow.GetDAO(typeof(IDAOStatistic))).UpdateAll(statsUpdated);
		}
	}
}
