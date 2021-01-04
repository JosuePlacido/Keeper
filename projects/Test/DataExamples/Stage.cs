using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataExamples
{
	public static class StageDataExamples
	{
		private static Stage[] Valid = new Stage[] {
			new Stage() {
				Name = "Fase Tal",
				Criterias = "0,1,2,3,4,5"
			}
		};

		public static Stage[] GetValidBasic()
		{
			return Valid;
		}
	}
}
