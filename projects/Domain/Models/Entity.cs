using System;
using System.Collections.Generic;

namespace Domain.Models
{
	public class Entity
	{
		public Entity(string id)
		{
			Id = id;
		}
		protected Entity() { }
		public string Id { get; private set; }

		public override bool Equals(object obj)
		{
			return obj is Entity @entity && Id == @entity.Id;
		}
		public override int GetHashCode()
		{
			return 2108858624 + EqualityComparer<string>.Default.GetHashCode(Id);
		}
		public override string ToString()
		{
			return $"{GetType()} {Id}";
		}
	}
}
