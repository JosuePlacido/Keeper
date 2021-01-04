using Domain.Models;
using Test.DataExamples;
using Xunit;

namespace Test.UnitTest.Infra.Repositorys
{
	internal class ValidPlayerSetup : TheoryData<Player>
	{
		public ValidPlayerSetup()
		{
			foreach (var player in PlayerDataExamples.GetValidBasic())
			{
				Add(player);
			}
		}
	}
}
