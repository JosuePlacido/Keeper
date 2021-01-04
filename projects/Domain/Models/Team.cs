using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class Team : Base
	{
		public virtual string Name { get; set; }
		public virtual string Abrev { get; set; }
		public virtual string LogoUrl { get; set; }
		public Team() { }
		public override string ToString()
		{
			return Name;
		}
	}
}
