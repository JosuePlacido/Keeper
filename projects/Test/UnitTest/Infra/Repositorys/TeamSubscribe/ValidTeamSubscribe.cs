using Domain.Models;
using Test.DataExamples;
using Xunit;

namespace Test.UnitTest.Infra.Repositorys
{
	internal class ValidTeamSubscribeSetup : TheoryData<TeamSubscribe>
	{
		public ValidTeamSubscribeSetup()
		{
			foreach (var team_subscribe in TeamSubscribeDataExamples.GetValidBasic())
			{
				Add(team_subscribe);
			}
		}
	}
}
