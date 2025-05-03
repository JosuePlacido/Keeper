using Domain.Models;
using MediatR;

namespace Domain.Events
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
