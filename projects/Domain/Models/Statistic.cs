using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;
using Domain.Enum;

namespace Domain.Models
{
	public class Statistics : Base
	{
		public virtual string GroupId { get; set; }
		public virtual Group Group { get; set; }
		public virtual string TeamSubscribeId { get; set; }
		public virtual TeamSubscribe TeamSubscribe { get; set; }
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
		public virtual int Points { get; set; }
		public virtual string Lastfive { get; set; }
		public virtual RankMovement RankMovement { get; set; }
		public Statistics() { }
		public override string ToString()
		{
			return Id;
		}
	}
}
