using Xunit;
using Xunit.Abstractions;
using Domain.Models;
using Infrastructure.DAO;

namespace Test.UnitTest.Infra.DAO
{
	public class TestValidationSquad : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public TestValidationSquad(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);

		[Theory]
		[ClassData(typeof(SquadValidationSetup))]
		public void TestSquadValidation(PlayerSubscribe test, bool expected)
		{
			string result;
			using (var context = Fixture.CreateContext())
			{
				result = new DAOPlayerSubscribe(context).ValidateUpdateOnSquad(test).Result;
			}
			Assert.Equal(expected, string.IsNullOrEmpty(result));
		}
	}
	public class SquadValidationSetup : TheoryData<PlayerSubscribe, bool>
	{
		public SquadValidationSetup()
		{
			Add(PlayerSubscribe.Factory("noexist", "p1", "false"), false);
			Add(PlayerSubscribe.Factory("noexist", "p1", "ts1"), true);
			Add(PlayerSubscribe.Factory("ps4", "noexist", "ts1"), false);
			Add(PlayerSubscribe.Factory("noexist", "p6", "ts1"), false);
			Add(PlayerSubscribe.Factory("ps2", "p11", "ts1"), false);
			Add(PlayerSubscribe.Factory("noexist", "p11", "ts1"), true);
			Add(PlayerSubscribe.Factory("ps1", "p5", "ts1"), true);
			Add(PlayerSubscribe.Factory("ps1", "p5", "ts2"), true);
		}
	}
}
