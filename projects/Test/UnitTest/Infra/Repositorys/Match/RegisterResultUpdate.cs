using System.Linq;
using Keeper.Domain.Enum;
using Keeper.Domain.Models;
using Keeper.Infrastructure.Repository;
using Keeper.Test;
using Xunit;
using Xunit.Abstractions;

namespace Keeper.Test.UnitTest.Infra.Repositorys
{
	public class RegisterResultUpdate : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public RegisterResultUpdate(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);

		[Fact]
		public void RegisterResult()
		{
			PlayerSubscribe player;
			TeamSubscribe team;
			Match match;
			EventGame[] events = new EventGame[]{
				EventGame.Factory("ev7",0,"gol visitante", TypeEvent.Goal,false,"m2","ps1"),
				EventGame.Factory("ev8",0,"gol visitante", TypeEvent.Goal,false,"m2","ps1"),
			};
			using (var context = Fixture.CreateContext())
			{
				var repo = new MatchRepository(context);
				match = repo.GetByIdWithTeamsAndPlayers("m2").Result;
				match.RegisterResult(0, 2, events: events);
				match = repo.RegisterResult(match).Result;
				context.SaveChanges();
				player = context.PlayerSubscribe.Where(ps => ps.Id == "ps1").FirstOrDefault();
				team = context.TeamSubscribes.Where(ps => ps.Id == "ts1").FirstOrDefault();
			}
			Assert.Equal(2, player.Goals);
			Assert.Equal(2, match.GoalsAway);
			Assert.Equal(1, team.Won);
			Assert.Equal(2, team.GoalsScores);
			Assert.Equal(2, team.GoalsDifference);
		}
	}
}
