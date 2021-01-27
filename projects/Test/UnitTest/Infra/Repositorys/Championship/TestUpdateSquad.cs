using System.Linq;
using Xunit;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Keeper.Infrastructure.Repository;
using Keeper.Domain.Models;
using Keeper.Domain.Enum;
using System.Threading.Tasks;

namespace Keeper.Test.UnitTest.Infra.Repositorys
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
				PlayerSubscribe.Factory("new", "p11", "ts1", status: Status.Matching),
				PlayerSubscribe.Factory("ps1", "p5", "ts1", status: Status.FreeAgent),
				PlayerSubscribe.Factory("ps2", "p6", "ts2", status: Status.Matching),
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
						Assert.Equal(test.Status, result.Status);
					}
					transaction.Rollback();
				}
			}
		}
	}
}
