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
		public virtual string TeamId { get; set; }
		public virtual Status Status { get; set; }
		public virtual Team Team { get; set; }
		[NotMapped]
		public virtual IEnumerable<PlayerSubscribe> Players { get; set; }
		public virtual IEnumerable<Statistics> Statistics { get; set; }
		public TeamSubscribe() { }

		public override string ToString()
		{
			return Id;
		}
	}
}
