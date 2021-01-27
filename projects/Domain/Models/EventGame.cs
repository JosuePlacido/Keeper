using Keeper.Domain.Enum;
using Keeper.Domain.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Keeper.Domain.Models
{
	public class EventGame : Entity
	{
		public int Order { get; private set; }
		public string Description { get; private set; }
		public TypeEvent Type { get; private set; }
		public bool IsHomeEvent { get; private set; }
		public string MatchId { get; set; }
		public Match Match { get; set; }
		public string RegisterPlayerId { get; set; }
		public PlayerSubscribe RegisterPlayer { get; set; }
		private EventGame() { }

		public override string ToString()
		{
			return Description;
		}

		public static EventGame Factory(string id, int order, string description,
			TypeEvent type, bool isHomeEvent,
			string matchId, string registerPlayerId,
			Match match = null, PlayerSubscribe registerPlayer = null)
		{
			return new EventGame
			{
				Id = id,
				Order = order,
				Description = description,
				Type = type,
				IsHomeEvent = isHomeEvent,
				MatchId = matchId,
				Match = match,
				RegisterPlayerId = registerPlayerId,
				RegisterPlayer = registerPlayer,
			};
		}
	}
}
