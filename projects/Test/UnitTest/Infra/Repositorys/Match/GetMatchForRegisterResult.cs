using Xunit;
using Xunit.Abstractions;
using Keeper.Infrastructure.Repository;
using Keeper.Domain.Models;

namespace Keeper.Test.UnitTest.Infra.Repositorys
{
	public class GetMatchForRegisterResult : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public GetMatchForRegisterResult(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);

		[Fact]
		public void GetsRegisterResult()
		{
			Match match;
			using (var context = Fixture.CreateContext())
			{
				match = new MatchRepository(context).GetByIdWithTeamsAndPlayers("m1").Result;
			}
			Assert.NotNull(match);
			Assert.NotNull(match.Home);
			Assert.NotNull(match.Away);
			Assert.NotNull(match.Home.Players);
			Assert.NotNull(match.Away.Players);
			Assert.NotNull(match.EventGames);
		}
	}
}
