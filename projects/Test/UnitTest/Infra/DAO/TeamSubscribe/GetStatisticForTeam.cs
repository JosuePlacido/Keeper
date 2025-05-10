using Xunit;
using Xunit.Abstractions;
using Domain.Models;
using Infrastructure.DAO;
using System.Linq;

namespace Test.UnitTest.Infra.DAO
{
	public class TestGetStatisticForTeam : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public TestGetStatisticForTeam(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);

		[Fact]
		public void TestGetStatisticTeam()
		{
			TeamSubscribe[] expected = SeedData.TeamsSubscribes;
			TeamSubscribe[] result;
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOTeamSubscribe(context);
				result = dao.GetByChampionshipTeamStatistics("c1").Result;
			}
			Assert.All(result, r => expected.Contains(r));
		}
	}
}
