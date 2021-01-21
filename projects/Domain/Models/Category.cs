using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Keeper.Domain.Core;

namespace Keeper.Domain.Models
{
	public sealed class Category : ValueObject<Category>
	{
		public string Name { get; private set; }
		private Category() { }
		public Category(string name)
		{
			Name = name;
		}
		public override string ToString()
		{
			return Name;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Name;
		}
	}
}
