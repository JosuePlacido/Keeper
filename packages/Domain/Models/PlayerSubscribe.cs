using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class PlayerSubscribe : Base
	{
		public virtual string TeamSubscribeId { get; set; }
		public virtual TeamSubscribe TeamSubscribe { get; set; }
		public virtual string PlayerId { get; set; }
		public virtual Player Player { get; set; }
		public virtual int Games { get; set; }
		public virtual int Goals { get; set; }
		public virtual int YellowCard { get; set; }
		public virtual int RedCard { get; set; }
		public virtual int MVPs { get; set; }
		public virtual string ChampionshipId { get; set; }
		public virtual Championship Championship { get; set; }
		public PlayerSubscribe() { }
		public override string ToString()
		{
			return Id;
		}
	}
}
