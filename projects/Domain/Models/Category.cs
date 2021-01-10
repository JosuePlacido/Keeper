using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public sealed class Category : Entity
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		private Category() { }
		public Category(string name, string description = null)
		{
			Name = name;
			Description = description;
		}
		public override string ToString()
		{
			return Name;
		}
	}
}
