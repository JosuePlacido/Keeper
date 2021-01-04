using Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class EventGame : Base
	{
		public virtual int Order { get; set; }
		public TypeEvent Type { get; set; }
		public virtual string MatchId { get; set; }
		public virtual Match Match { get; set; }
		public virtual string RegisterPlayerId { get; set; }
		public virtual PlayerSubscribe RegisterPlayer { get; set; }
		public virtual string Description { get; set; }
		public virtual bool IsHomeEvent { get; set; }
		public EventGame() { }
		public override string ToString()
		{
			return Id;
		}
	}
}
