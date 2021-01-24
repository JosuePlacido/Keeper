
using System.Linq;
using Keeper.Application.Services;
using AutoMapper;
using Xunit;
using Xunit.Abstractions;
using Keeper.Infrastructure.Repository;
using Keeper.Infrastructure.DAO;
using System.Collections.Generic;
using Keeper.Domain.Models;
using Keeper.Infrastructure.CrossCutting.DTO;

namespace Keeper.Test.UnitTest.Application.Service
{
	public class PlayerServiceList : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		public SharedDatabaseFixture Fixture { get; }
		public PlayerServiceList(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (_output, Fixture) = (output, fixture);

		[Fact]
		public void Get_PlayerList_PageAndTakes()
		{
			var expected = SeedData.Players;
			int pages = expected.Length / 5;
			List<Player> finalList = new List<Player>();
			using (var context = Fixture.CreateContext())
			{
				PlayerRepository repo = new PlayerRepository(context);
				DAOPlayer dao = new DAOPlayer(context);
				var service = new PlayerService(null, repo, dao);
				PlayerPaginationDTO result = null;
				for (int p = 1; p <= pages; p++)
				{
					result = service.GetAvailables(page: p, take: 5).Result;
					Assert.Equal(result.Total, expected.Length);
					Assert.Equal(result.Page, p);
					Assert.Equal(result.Players.Length, 5);
					finalList.AddRange(result.Players);
				}
				result = service.GetAvailables(page: 3, take: 5).Result;
				Assert.True(result.Total == expected.Length);
				Assert.True(result.Page == 3);
				Assert.True(result.Players.Length == 1);
				finalList.AddRange(result.Players);
				Assert.True(expected.Length == finalList.Count);
				Assert.All(result.Players, item => expected.Contains(item));
				Assert.All(result.Players, item => expected.Contains(item));
			}
		}
		[Fact]
		public void Get_PlayerList_WithTerms()
		{
			using (var context = Fixture.CreateContext())
			{
				PlayerRepository repo = new PlayerRepository(context);
				var result = new PlayerService(null, repo, new DAOPlayer(context))
					.GetAvailables("player").Result;
				var expected = SeedData.Players.Take(4);
				Assert.True(result.Total == 4);
				Assert.True(result.Terms == "player");
				Assert.True(result.Players.Length == expected.Count());
				Assert.All(result.Players, item => expected.Contains(item));
			}
		}
		[Fact]
		public void Get_PlayerList_NotInChampionship()
		{
			using (var context = Fixture.CreateContext())
			{
				string championship = new ChampionshipRepository(context).GetAll().Result.LastOrDefault().Id;
				PlayerRepository repo = new PlayerRepository(context);
				var result = new PlayerService(null, repo, new DAOPlayer(context))
					.GetAvailables(championship: championship).Result;
				var expected = SeedData.Players;
				Assert.Equal(result.Total, 5);
				Assert.Equal(result.ExcludeFromChampionship, championship);
				Assert.Equal(result.Players.Length, 5);
				Assert.All(result.Players, item => expected.Contains(item));
			}
		}
	}
}
