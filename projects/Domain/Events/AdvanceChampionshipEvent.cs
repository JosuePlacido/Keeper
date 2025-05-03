using MediatR;

namespace Domain.Events
{
	public class AdvanceChampionshipEvent : INotification
	{
		public string Group { get; set; }
		public int Round { get; set; }

		public AdvanceChampionshipEvent(string group, int round)
		{
			Group = group;
			Round = round;
		}
	}
}
