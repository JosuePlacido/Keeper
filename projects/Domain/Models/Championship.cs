using System;
using System.Collections.Generic;
using System.Linq;
using Keeper.Domain.Enum;
using Keeper.Domain.Core;

namespace Keeper.Domain.Models
{
	public class Championship : Entity, IAggregateRoot
	{
		public string Name { get; private set; }
		public string Edition { get; private set; }
		public Category Category { get; private set; }
		public string Status { get; private set; }
		public IList<Stage> Stages { get; private set; } = new List<Stage>();
		public IList<TeamSubscribe> Teams { get; private set; } = new List<TeamSubscribe>();

		protected Championship() { }

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

		public Championship(string name, string edition, Category category, string status)
		{
			Name = name;
			Edition = edition;
			Category = category;
			Status = status;
		}
		public Championship(string name, string edition, Category category
			, string status, IList<Stage> stages, IList<TeamSubscribe> teams)
		{
			Name = name;
			Edition = edition;
			Category = category;
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
