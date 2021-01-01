using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class Match : Base
	{
		public virtual string Name { get; set; }
		public virtual string Local { get; set; }
		public virtual DateTime? Date { get; set; }
		public virtual string GroupId { get; set; }
		public virtual Group Group { get; set; }
		public virtual HookPlaces HookHome { get; set; }
		public virtual string HookHomeId { get; set; }
		public virtual HookPlaces HookAway { get; set; }
		public virtual string HookAwayId { get; set; }
		public virtual string HomeId { get; set; }
		public virtual TeamSubscribe Home { get; set; }
		public virtual string AwayId { get; set; }
		public virtual TeamSubscribe Away { get; set; }
		public virtual int Round { get; set; }
		public virtual int? GoalsHome { get; set; }
		public virtual int? GoalsAway { get; set; }
		public virtual int? GoalsPenaltyHome { get; set; }
		public virtual int? GoalsPenaltyVisitante { get; set; }
		public virtual bool FinalGame { get; set; }
		public virtual bool AggregateGame { get; set; }
		public virtual bool Penalty { get; set; }
		public virtual int? AggregateGoalsAway { get; set; }
		public virtual int? AggregateGoalsHome { get; set; }
		public virtual Status Status { get; set; }
		public virtual IEnumerable<EventGame> EventGame { get; set; }
		public Match() { }
		public override string ToString()
		{
			return Name;
		}
	}
}
