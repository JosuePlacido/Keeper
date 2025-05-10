using System.Linq;
using Xunit;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Infrastructure.Repository;
using Domain.Models;
using Application.Contract.DAL;
using Infrastructure.DAO;
using Domain.Enum;

namespace Test.UnitTest.Infra.DAO
{
	public class TestGetStatisticForPlayer : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public TestGetStatisticForPlayer(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);

		[Fact]
		public void TestGetStatisticPlayer()
		{
			PlayerSubscribe[] expected = SeedData.PlayersSubscribe;
			PlayerSubscribe[] result;
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOPlayerSubscribe(context);
				result = dao.GetByChampionshipPlayerStatistics("c1").Result;
			}
			Assert.Equal(expected, result);
		}
	}
}
