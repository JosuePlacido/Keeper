using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class Player : Entity
	{
		public string Name { get; private set; }
		private Player() { }

		public Player(string name)
		{
			Name = name;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
