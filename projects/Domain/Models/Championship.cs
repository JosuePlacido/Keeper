using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Enum;

namespace Domain.Models
{
	public class Championship : Entity
	{
		public string Name { get; private set; }
		public string Edition { get; private set; }
		public Category Category { get; private set; }
		public string CategoryId { get; private set; }
		public string Status { get; private set; }
		public IList<Stage> Stages { get; private set; }
		public IList<TeamSubscribe> Teams { get; private set; }

		private Championship() { }

		public void EditScope(string name = null, string edition = null, string status = null)
		{
			if (name != null)
			{
				Name = name;
			}
			if (edition != null)
			{
				Edition = edition;
			}
			if (status != null)
			{
				Status = status;
			}
		}

		public Championship(string name, string edition, string categoryId, string status)
		{
			Name = name;
			Edition = edition;
			CategoryId = categoryId;
			Status = status;
		}
		public Championship(string name, string edition, string categoryId
			, string status, IList<Stage> stages, IList<TeamSubscribe> teams)
		{
			Name = name;
			Edition = edition;
			CategoryId = categoryId;
			Status = status;
			Stages = stages;
			Teams = teams;
		}

		public override string ToString()
		{
			return $"{Name} {Edition}";
		}
	}
}
