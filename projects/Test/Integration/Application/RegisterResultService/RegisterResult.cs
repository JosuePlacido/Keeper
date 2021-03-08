
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;
using Keeper.Application.Services.RegisterResult;
using Keeper.Domain.Enum;
using Keeper.Application.Contract;
using MediatR;
using Keeper.Domain.Events;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
				var service = new RegisterResultService(null, new UnitOfWork(context, null));
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
				var service = new RegisterResultService(null, new UnitOfWork(context, null));
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
			TeamSubscribe[] team;
			IServiceResponse result;
			using (var context = Fixture.CreateContext())
			{
				var service = new RegisterResultService(null, new UnitOfWork(context, new Moq.Mock<IMediator>().Object));
				result = service.RegisterResult(test).Result;
				player = context.PlayerSubscribe.Where(ps => ps.Id == "ps1").FirstOrDefault();
				team = context.TeamSubscribes.Where(ps => ps.Id == "ts1" || ps.Id == "ts2")
					.ToArray();
			}
			Assert.True(result.ValidationResult.IsValid);
			Match match = (Match)result.Value;
			Assert.Equal(2, player.Goals);
			Assert.Equal(2, player.Games);
			Assert.Equal(1, player.YellowCard);

			Assert.Equal(1, team[0].Won);
			Assert.Equal(1, team[0].Drowns);
			Assert.Equal(2, team[0].Games);
			Assert.Equal(2, team[0].GoalsScores);
			Assert.Equal(2, team[0].GoalsDifference);
			Assert.Equal(1, team[1].Lost);
			Assert.Equal(1, team[1].Drowns);
			Assert.Equal(2, team[1].Games);
			Assert.Equal(2, team[1].GoalsAgainst);
			Assert.Equal(-2, team[1].GoalsDifference);

			Assert.Equal(2, match.GoalsAway);
			Assert.Equal(2, match.AggregateGoalsAway);
		}
		[Fact]
		public void TestRegisterResultHandler_UpdateTeamStatistics()
		{
			Match match = SeedData.Matches[1];
			match.RegisterResult(0, 2);
			RegisterResultDomainEventHandler handler;
			RegisterResultEvent eventHandler = new RegisterResultEvent(match);
			using (var context = Fixture.CreateContext())
			{
				handler = new RegisterResultDomainEventHandler(new UnitOfWork(context, new Moq.Mock<IMediator>().Object));
				var cltToken = new System.Threading.CancellationToken();
				Task.Run(async () => await handler.Handle(eventHandler, cltToken)).Wait();
				context.SaveChanges();
				var cru = context.Statistics.Where(g => g.Id == "s2").FirstOrDefault();
				var spfc = context.Statistics.Where(g => g.Id == "s1").FirstOrDefault();
				Assert.Equal(2, cru.Position);
				Assert.Equal(1, spfc.Position);

				Assert.Equal(1, cru.Lost);
				Assert.Equal(1, cru.Drowns);
				Assert.Equal(2, cru.Games);
				Assert.Equal(2, cru.GoalsAgainst);
				Assert.Equal(-2, cru.GoalsDifference);
				Assert.Equal("draw,lose", cru.Lastfive);
				Assert.Equal(-1, cru.RankMovement);

				Assert.Equal(1, spfc.Won);
				Assert.Equal(1, spfc.Drowns);
				Assert.Equal(2, spfc.Games);
				Assert.Equal(2, spfc.GoalsScores);
				Assert.Equal(2, spfc.GoalsDifference);
				Assert.Equal("draw,win", spfc.Lastfive);
				Assert.Equal(0, spfc.RankMovement);

			}
		}
		[Fact]
		public void TestRegisterResultHandler_UpdateChampionship()
		{
			Championship champ;
			TeamSubscribe cru;
			TeamSubscribe spfc;
			Group group;
			Match match = SeedData.Matches[1];
			match.RegisterResult(0, 2);
			UpdateChampionshipDomainEventHandler handler;
			UpdateChampionshipEvent eventHandler = new UpdateChampionshipEvent("g1", 2);
			using (var context = Fixture.CreateContext())
			{
				handler = new UpdateChampionshipDomainEventHandler(new UnitOfWork(context, new Moq.Mock<IMediator>().Object));
				var cltToken = new System.Threading.CancellationToken();
				Task.Run(async () => await handler.Handle(eventHandler, cltToken)).Wait();
				context.SaveChanges();
				champ = context.Championships.Where(g => g.Id == "c1").FirstOrDefault();
				spfc = context.TeamSubscribes.Where(g => g.Id == "ts1").FirstOrDefault();
				cru = context.TeamSubscribes.Where(g => g.Id == "ts2").FirstOrDefault();
				group = context.Groups.Where(g => g.Id == "g1").FirstOrDefault();
			}
			Assert.Equal(Status.Finish, champ.Status);
			Assert.Equal(Status.Eliminated, cru.Status);
			Assert.Equal(Status.Champion, spfc.Status);
			Assert.Equal(2, group.CurrentRound);
		}
	}
}
