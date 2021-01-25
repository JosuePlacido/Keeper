using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Keeper.Domain.Core;
using Keeper.Domain.Utils;

namespace Keeper.Domain.Models
{
	public class Team : Entity, IAggregateRoot
	{
		private Team()
		{
		}

		public string Name { get; private set; }
		private string _abrev;
		public string Abrev
		{
			get { return _abrev; }
			private set
			{
				_abrev = !string.IsNullOrEmpty(value) ?
	  				StringUtils.NormalizeLower(value).ToUpper() : value;
			}
		}
		public string LogoUrl { get; private set; }

		public Team(string name, string abrev = null, string logoUrl = null)
		{
			Name = name;
			Abrev = abrev;
			LogoUrl = logoUrl;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
