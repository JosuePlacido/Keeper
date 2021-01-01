using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class Player : Base
	{

		public virtual string Name { get; set; }
		public Player() { }

		public override string ToString()
		{
			return Name;
		}
	}
}
