using Keeper.Domain.Core;
using Keeper.Domain.Models;
using MediatR;

namespace Keeper.Domain.Events
{
	public class UpdateChampionshipEvent : INotification
	{
		public string Group { get; set; }
		public int Round { get; set; }

		public UpdateChampionshipEvent(string group, int round)
		{
			Group = group;
			Round = round;
		}
	}
}
