using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataExamples
{
	public static class ChampionshipDataExamples
	{
		private static Championship[] ValidCreate = new Championship[] {
			new Championship() {
				Name = "Recopa Sulamericana",
				Edition = "1993",
				Status = Status.Finish,
			}
		};

		public static Championship[] GetValidBasic()
		{
			return ValidCreate;
		}
	}
}
