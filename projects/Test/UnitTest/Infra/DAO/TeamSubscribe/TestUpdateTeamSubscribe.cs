using System.Linq;
using Xunit;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Keeper.Infrastructure.Repository;
using Keeper.Domain.Models;
using Keeper.Application.Contract;
using Keeper.Infrastructure.DAO;
using Keeper.Domain.Enum;

namespace Keeper.Test.UnitTest.Infra.DAO
{
	public class TestUpdateTeamSubscribe : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public TestUpdateTeamSubscribe(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);

		[Fact]
		public void TestUpdateTeam()
		{
			TeamSubscribe[] expected = SeedData.TeamsSubscribes;
			foreach (var item in expected)
			{
				item.UpdateNumbers(games: 5);
			}
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOTeamSubscribe(context);
				dao.UpdateAll(expected);
			}
			Assert.All(expected, i => Assert.Equal(5, i.Games));
		}
	}
}
