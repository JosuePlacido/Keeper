using Domain.Models;
using Test.DataExamples;
using Xunit;

namespace Test.UnitTest.Infra.Repositorys
{
	internal class ValidPlayerSubscribeSetup : TheoryData<PlayerSubscribe>
	{
		public ValidPlayerSubscribeSetup()
		{
			foreach (var player_subscribe in PlayerSubscribeDataExamples.GetValidBasic())
			{
				Add(player_subscribe);
			}
		}
	}
}
