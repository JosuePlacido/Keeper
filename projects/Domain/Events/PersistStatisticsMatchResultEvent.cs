using Domain.Models;
using MediatR;

namespace Domain.Events
{
	public class PersistStatisticsMatchResultEvent : INotification
	{
		public Match Match { get; set; }

		public PersistStatisticsMatchResultEvent(Match match)
		{
			Match = match;
		}
	}
}
