
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Keeper.Infrastructure.Data;
using Keeper.Application.Services.RegisterResult;
using Keeper.Domain.Enum;
using Keeper.Application.Contract;

namespace Keeper.Test.Integration.Application
{
	public class TestRegisterResultApplication : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		public SharedDatabaseFixture Fixture { get; }
		public TestRegisterResultApplication(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (_output, Fixture) = (output, fixture);

		[Fact]
		public void GetMatch()
		{
			Match match;
			using (var context = Fixture.CreateContext())
			{
				var service = new RegisterResultService(null, new UnitOfWork(context));
				match = service.GetMatch("m1").Result;
			}
			Assert.Equal(SeedData.Matches[0], match);
		}
		[Fact]
		public void Get_Invalid_Match()
		{
			Match match;
			using (var context = Fixture.CreateContext())
			{
				var service = new RegisterResultService(null, new UnitOfWork(context));
				match = service.GetMatch("noexist").Result;
			}
			Assert.Null(match);
		}
		[Fact]
		public void RegisterResul()
		{
			MatchResultDTO test = new MatchResultDTO
			{
				Id = "m2",
				GoalsHome = 0,
				GoalsAway = 2,
				Events = new EventGameDTO[]{
					new EventGameDTO{
						Description = "Gol visitante",
						PlayerId = "ps1",
						IsHomeEvent = false,
						Type = TypeEvent.Goal,
						MatchId = "m2"
					},
					new EventGameDTO{
						Description = "Gol visitante",
						PlayerId = "ps1",
						IsHomeEvent = false,
						Type = TypeEvent.Goal,
						MatchId = "m2"
					},
				}
			};
			PlayerSubscribe player;
			TeamSubscribe team;
			IServiceResponse result;
			using (var context = Fixture.CreateContext())
			{
				var service = new RegisterResultService(null, new UnitOfWork(context));
				result = service.RegisterResult(test).Result;
				player = context.PlayerSubscribe.Where(ps => ps.Id == "ps1").FirstOrDefault();
				team = context.TeamSubscribes.Where(ps => ps.Id == "ts1").FirstOrDefault();
			}
			Assert.True(result.ValidationResult.IsValid);
			Match match = (Match)result.Value;
			Assert.Equal(2, player.Goals);
			Assert.Equal(2, match.GoalsAway);
			Assert.Equal(1, team.Won);
			Assert.Equal(2, team.GoalsScores);
			Assert.Equal(2, team.GoalsDifference);
		}
	}
}
