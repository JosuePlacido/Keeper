using Domain.Models;
using Test.DataExamples;
using Xunit;

namespace Test.UnitTest.Infra.Repositorys
{
	internal class ValidStatisticsSetup : TheoryData<Statistics>
	{
		public ValidStatisticsSetup()
		{
			foreach (var statistics in StatisticsDataExamples.GetValidBasic())
			{
				Add(statistics);
			}
		}
	}
}
