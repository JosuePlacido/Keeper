using Domain.Models;
using Test.DataExamples;
using Xunit;

namespace Test.UnitTest.Infra.Repositorys
{
	internal class ValidMatchSetup : TheoryData<Match>
	{
		public ValidMatchSetup()
		{
			foreach (var match in MatchDataExamples.GetValidBasic())
			{
				Add(match);
			}
		}
	}
}
