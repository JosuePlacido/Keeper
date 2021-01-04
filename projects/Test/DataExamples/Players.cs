using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataExamples
{
	public static class PlayerDataExamples
	{
		private static Player[] Valid = new Player[] {
			new Player() {
				Name = "Fulano",
			},
			new Player() {
				Name = "Beltrano",
			},
			new Player() {
				Name = "Cicrano",
			},
		};

		public static Player[] GetValidBasic()
		{
			return Valid;
		}
	}
}
