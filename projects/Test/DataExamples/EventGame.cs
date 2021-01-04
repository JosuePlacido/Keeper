using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataExamples
{
	public static class EventGameDataExamples
	{
		private static EventGame[] Valid = new EventGame[] {
			new EventGame() {
				Description = "Nada"
			}
		};

		public static EventGame[] GetValidBasic()
		{
			return Valid;
		}
	}
}
