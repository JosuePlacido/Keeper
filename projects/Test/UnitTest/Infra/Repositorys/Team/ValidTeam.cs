using Domain.Models;
using Test.DataExamples;
using Xunit;

namespace Test.UnitTest.Infra.Repositorys
{
	internal class ValidTeamSetup : TheoryData<Team>
	{
		public ValidTeamSetup()
		{
			foreach (var team in TeamDataExamples.GetValidBasic())
			{
				Add(team);
			}
		}
	}
}
