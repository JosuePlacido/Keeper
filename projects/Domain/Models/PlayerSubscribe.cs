using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Keeper.Domain.Core;

namespace Keeper.Domain.Models
{
	public class PlayerSubscribe : Entity
	{
		public string TeamSubscribeId { get; private set; }
		public string PlayerId { get; private set; }
		public Player Player { get; private set; }
		public int Games { get; private set; }
		public int Goals { get; private set; }
		public int YellowCard { get; private set; }
		public int RedCard { get; private set; }
		public int MVPs { get; private set; }
		public string Status { get; private set; }

		private PlayerSubscribe() { }
		public PlayerSubscribe(string player, string status = Enum.Status.Matching)
			: base(Guid.NewGuid().ToString())
		{
			PlayerId = player;
			Status = Status;
		}

		public override string ToString()
		{
			return (Player == null) ? base.ToString() : $"{Player.Name}";
		}
		public static PlayerSubscribe Factory(string id, string playerId,
			string teamSubscribeId = null, Player player = null, int games = 0,
			int goals = 0, int yellowCard = 0, int redCard = 0,
			int mVPs = 0, string status = Enum.Status.Matching)
		{
			return new PlayerSubscribe()
			{
				Id = id,
				PlayerId = playerId,
				Player = player,
				Games = games,
				Goals = goals,
				YellowCard = yellowCard,
				RedCard = redCard,
				MVPs = mVPs,
				Status = status,
			};
		}
	}
}
