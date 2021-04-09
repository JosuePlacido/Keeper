using Keeper.Domain.Core;
using Keeper.Domain.Models;
using MediatR;

namespace Keeper.Domain.Events
{
	public class UpdateChampionshipEvent : INotification
	{
		public Group Group { get; set; }
		public int Round { get; set; }

		public UpdateChampionshipEvent(Group group, int round)
		{
			Group = group;
			Round = round;
		}
	}
}
