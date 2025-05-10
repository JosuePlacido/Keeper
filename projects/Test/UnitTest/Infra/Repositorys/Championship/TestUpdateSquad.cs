using Xunit;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Infrastructure.Repository;
using Domain.Models;
using Domain.Enum;

namespace Test.UnitTest.Infra.Repositorys
{
	public class TestUpdateSquad : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public TestUpdateSquad(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);

		private void Print(object item) => _output.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented,
				new JsonSerializerSettings
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore
				}));
		[Fact]
		public void TestUpdateValidSquad()
		{
			PlayerSubscribe[] squad = new PlayerSubscribe[]{
				PlayerSubscribe.Factory("new", "p11", "ts1"),
				PlayerSubscribe.Factory("ps1", "p5", "ts1"),
				PlayerSubscribe.Factory("ps2", "p6", "ts2"),
			};
			using (var transaction = Fixture.Connection.BeginTransaction())
			{
				using (var context = Fixture.CreateContext(transaction))
				{
					ChampionshipRepository repository = new ChampionshipRepository(context);
					PlayerSubscribe result = null;
					foreach (var test in squad)
					{
						result = repository.UpdatePLayer(test).Result;
						context.SaveChanges();
						Assert.NotNull(result);
						Assert.Equal(test.TeamSubscribeId, result.TeamSubscribeId);
					}
					transaction.Rollback();
				}
			}
		}
	}
}
