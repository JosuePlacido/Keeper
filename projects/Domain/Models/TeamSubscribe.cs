using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enum;

namespace Domain.Models
{
	public class TeamSubscribe : Base
	{
		public virtual string ChampionshipId { get; set; }
		public virtual Championship Championship { get; set; }
		public virtual string Status { get; set; }
		public virtual string TeamId { get; set; }
		public virtual Team Team { get; set; }
		public virtual int Games { get; set; }
		public virtual int Won { get; set; }
		public virtual int Drowns { get; set; }
		public virtual int Lost { get; set; }
		public virtual int GoalsScores { get; set; }
		public virtual int Position { get; set; }
		public virtual int GoalsAgainst { get; set; }
		public virtual int GoalsDifference { get; set; }
		public virtual int Yellows { get; set; }
		public virtual int Reds { get; set; }
		public virtual IEnumerable<PlayerSubscribe> Players { get; set; }

		public TeamSubscribe() { }

		public override string ToString()
		{
			return Id;
		}
	}
}
