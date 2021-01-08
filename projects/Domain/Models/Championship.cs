using System;
using System.Collections.Generic;
using System.Linq;
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
		public virtual IEnumerable<TeamSubscribe> Teams { get; set; }
		public virtual string Status { get; set; }
		public Championship(string name, string edition, string categoryId, string status)
		{
			Name = name;
			Edition = edition;
			CategoryId = categoryId;
			Status = status;
		}

		public Championship()
		{
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
