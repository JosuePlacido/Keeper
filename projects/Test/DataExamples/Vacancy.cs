using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataExamples
{
	public static class VacancyDataExamples
	{
		private static Vacancy[] Valid = new Vacancy[] {
			new Vacancy() {
				Id = "teste",
				Description = "Teste",
			}
		};

		public static Vacancy[] GetValidBasic()
		{
			return Valid;
		}
	}
}
