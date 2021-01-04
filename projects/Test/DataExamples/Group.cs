using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataExamples
{
	public static class GroupDataExamples
	{
		private static Group[] Valid = new Group[] {
			new Group() {
				Name = "TESTE",
			}
		};

		public static Group[] GetValidBasic()
		{
			return Valid;
		}
	}
}
