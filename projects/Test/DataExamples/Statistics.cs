using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataExamples
{
	public static class StatisticsDataExamples
	{
		private static Statistics[] Valid = new Statistics[] {
			new Statistics() {
				Lastfive = "TESTE"
			}
		};

		public static Statistics[] GetValidBasic()
		{
			return Valid;
		}
	}
}
