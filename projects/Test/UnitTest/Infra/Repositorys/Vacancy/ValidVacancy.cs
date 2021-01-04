using Domain.Models;
using Test.DataExamples;
using Xunit;

namespace Test.UnitTest.Infra.Repositorys
{
	internal class ValidVacancySetup : TheoryData<Vacancy>
	{
		public ValidVacancySetup()
		{
			foreach (var vacancy in VacancyDataExamples.GetValidBasic())
			{
				Add(vacancy);
			}
		}
	}
}
