using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataExamples
{
	public static class PlayerSubscribeDataExamples
	{
		private static PlayerSubscribe[] Valid = new PlayerSubscribe[] {
			new PlayerSubscribe() {
				Id = "teste",
				Games = -1
			}
		};

		public static PlayerSubscribe[] GetValidBasic()
		{
			return Valid;
		}
	}
}
