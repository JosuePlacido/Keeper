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

		private PlayerSubscribe() { }
		public PlayerSubscribe(string player) : base(Guid.NewGuid().ToString())
		{
			PlayerId = player;
		}
		public PlayerSubscribe UpdateNumbers(int games, int goals, int yellowCard,
			int redCard, int mVPs)
		{
			Games = games;
			Goals = goals;
			YellowCard = yellowCard;
			RedCard = redCard;
			MVPs = mVPs;
			return this;
		}

		public override string ToString()
		{
			return (Player == null) ? base.ToString() : $"{Player.Name}";
		}
	}
}
