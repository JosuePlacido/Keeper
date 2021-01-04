using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataExamples
{
	public static class MatchDataExamples
	{
		private static Match[] Valid = new Match[] {
			new Match() {
				Name = "Teste",
			}
		};

		public static Match[] GetValidBasic()
		{
			return Valid;
		}
	}
}
