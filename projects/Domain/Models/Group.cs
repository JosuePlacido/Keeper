using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;

namespace Domain.Models
{
	public class Group : Base
	{
		public virtual string Name { get; set; }
		public virtual string StageId { get; set; }
		public virtual Stage Stage { get; set; }
		public virtual IEnumerable<TeamSubscribe> Teams { get; set; }
		public virtual IEnumerable<Vacancy> Vacancys { get; set; }
		public virtual IEnumerable<Statistics> Statistics { get; set; }
		public virtual IEnumerable<Match> Matchs { get; set; }
		public Group() { }

		public override string ToString()
		{
			return Name;
		}
	}
}
