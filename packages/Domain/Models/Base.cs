using System;
using System.Collections.Generic;

namespace Domain.Models
{
	public class Base
	{
		protected Base() { }
		public string Id { get; set; }
		public override bool Equals(object obj)
		{
			return obj is Base @base &&
				   Id == @base.Id;
		}
		public override int GetHashCode()
		{
			return 2108858624 + EqualityComparer<string>.Default.GetHashCode(Id);
		}
	}
}
