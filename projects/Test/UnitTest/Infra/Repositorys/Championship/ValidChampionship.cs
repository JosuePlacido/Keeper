using Domain.Models;
using Test.DataExamples;
using Xunit;

namespace Test.UnitTest.Infra.Repositorys
{
	internal class ValidChampionshipSetup : TheoryData<Championship>
	{
		public ValidChampionshipSetup()
		{
			foreach (var Championship in ChampionshipDataExamples.GetValidBasic())
			{
				Add(Championship);
			}
		}
	}
}
