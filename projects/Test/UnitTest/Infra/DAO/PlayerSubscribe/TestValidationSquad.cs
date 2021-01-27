using System.Linq;
using Xunit;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Keeper.Infrastructure.Repository;
using Keeper.Domain.Models;
using Keeper.Application.DAO;
using Keeper.Infrastructure.DAO;
using Keeper.Domain.Enum;

namespace Keeper.Test.UnitTest.Infra.DAO
{
	public class TestValidationSquad : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public TestValidationSquad(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);

		private void Print(object item) => _output.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented,
				new JsonSerializerSettings
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore
				}));
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
			Add(PlayerSubscribe.Factory("noexist", "p1", "false", status: Status.Matching), false);
			Add(PlayerSubscribe.Factory("noexist", "p1", "ts1", status: Status.Matching), true);
			Add(PlayerSubscribe.Factory("ps4", "noexist", "ts1", status: Status.Matching), false);
			Add(PlayerSubscribe.Factory("noexist", "p6", "ts1", status: Status.Matching), false);
			Add(PlayerSubscribe.Factory("ps2", "p11", "ts1", status: Status.FreeAgent), false);
			Add(PlayerSubscribe.Factory("noexist", "p11", "ts1", status: Status.Matching), true);
			Add(PlayerSubscribe.Factory("ps1", "p5", "ts1", status: Status.FreeAgent), true);
			Add(PlayerSubscribe.Factory("ps1", "p5", "ts2", status: Status.Matching), true);
		}
	}
}
