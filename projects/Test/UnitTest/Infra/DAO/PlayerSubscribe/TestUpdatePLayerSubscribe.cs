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
	public class TestUpdatePlayerSubscribe : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public TestUpdatePlayerSubscribe(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);

		[Fact]
		public void TestUpdatePlayer()
		{
			PlayerSubscribe[] expected = SeedData.PlayersSubscribe;
			foreach (var item in expected)
			{
				item.UpdateNumbers(games: 5);
			}
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOPlayerSubscribe(context);
				dao.UpdateAll(expected);
			}
			Assert.All(expected, i => Assert.Equal(5, i.Games));
		}
	}
}
