using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Domain.Models
{
	public class Championship : Base
	{
		public virtual string Name { get; set; }
		public virtual string Edition { get; set; }
		public virtual Category Category { get; set; }
		public virtual string CategoryId { get; set; }
		public virtual IEnumerable<Stage> Stages { get; set; }
		public virtual Status Status { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
