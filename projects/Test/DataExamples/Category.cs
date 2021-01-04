using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataExamples
{
	public static class CategoryDataExamples
	{
		private static Category[] Valid = new Category[] {
			new Category() {
				Name = "Profissional",
			},
			new Category() {
				Name = "Amador",
			},
			new Category() {
				Name = "Sub-20",
			},
		};

		public static Category[] GetValidBasic()
		{
			return Valid;
		}
	}
}
