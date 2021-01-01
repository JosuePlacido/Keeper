using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class Category : Base
	{
		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
		public Category() { }
		public override string ToString()
		{
			return Name;
		}
	}
}
