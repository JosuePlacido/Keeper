using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataExamples
{
	public static class TeamDataExamples
	{
		private static Team[] Valid = new Team[] {
			new Team() {
				Name = "Botafogo",
			},
			new Team() {
				Name = "Vasco da Gama",
				Abrev = "VAS",
			},
			new Team() {
				Name = "Fluminense",
				Abrev = "FLU",
			},
			new Team() {
				Name = "Flamengo",
				LogoUrl = "https",
				Abrev = "FLA"
			},
		};
		private static Team[] Invalid = new Team[] {
			new Team() {
				Name = "",
			},
			new Team(),
			null,
		};

		public static Team[] GetValidBasic()
		{
			return Valid;
		}
	}
}
