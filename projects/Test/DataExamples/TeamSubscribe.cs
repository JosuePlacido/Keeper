using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataExamples
{
	public static class TeamSubscribeDataExamples
	{
		private static TeamSubscribe[] Valid = new TeamSubscribe[] {
			new TeamSubscribe() {
				Status = "TESTE"
			}
		};

		public static TeamSubscribe[] GetValidBasic()
		{
			return Valid;
		}
	}
}
