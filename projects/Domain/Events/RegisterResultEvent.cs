using Keeper.Domain.Core;
using Keeper.Domain.Models;
using MediatR;

namespace Keeper.Domain.Events
{
	public class RegisterResultEvent : INotification
	{
		public Match Match { get; set; }

		public RegisterResultEvent(Match match)
		{
			Match = match;
		}
	}
}
