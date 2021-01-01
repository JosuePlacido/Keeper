using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class Stage : Base
	{
		public virtual int Order { get; set; }
		public virtual string Name { get; set; }
		public virtual Championship Championship { get; set; }
		public virtual string ChampionshipId { get; set; }
		public virtual int Teams { get; set; }
		public virtual int SpotsNextStage { get; set; }
		public virtual TypeStage TypeStage { get; set; }
		public virtual bool IsDoubleTurn { get; set; }
		public virtual string Criterias { get; set; }
		public virtual Classifieds Regulation { get; set; }
		public virtual IEnumerable<Group> Group { get; set; }
		public Stage() { }
		public override string ToString()
		{
			return Name;
		}
	}
}
