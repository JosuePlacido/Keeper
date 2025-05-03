using Domain.Models;
using MediatR;

namespace Domain.Events
{
	public class RankingEvent : INotification
	{
		public Group Group { get; set; }
		public string CriteriaIds { get; set; }

		public RankingEvent(Group group, string criteriaIds)
		{
			Group = group;
			CriteriaIds = criteriaIds;
		}
	}
}
