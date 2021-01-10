﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class Team : Entity
	{
		private Team()
		{
		}

		public string Name { get; private set; }
		public string Abrev { get; private set; }
		public string LogoUrl { get; private set; }

		public Team(string name, string abrev = null, string logoUrl = null)
		{
			Name = name;
			Abrev = abrev;
			LogoUrl = logoUrl;
		}

		public override string ToString()
		{
			return "{Name}";
		}
	}
}
