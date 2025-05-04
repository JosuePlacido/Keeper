
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.Repository;
using Infrastructure.DAO;
using System.Collections.Generic;
using Domain.Models;
using Newtonsoft.Json;
using Infrastructure.Data;
using Application.Services.CRUDPlayer;

namespace Test.Integration.Application
{
	public class PlayerAvailableServiceList : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		public SharedDatabaseFixture Fixture { get; }
		public PlayerAvailableServiceList(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (_output, Fixture) = (output, fixture);

		[Fact]
		public void Get_PlayerList_PageAndTakes()
		{
			Fixture.RunInTransaction(context =>
			{
				PlayerRepository repo = new PlayerRepository(context);
				Player[] expected = repo.GetAll().Result;
				int pages = expected.Length / 5;
				int LastPage = expected.Length % 5;
				List<Player> finalList = new List<Player>();
				DAOPlayer dao = new DAOPlayer(context);
				var service = new PlayerService(null, new UnitOfWork(context, null));
				PlayerAvailablePaginationDTO result = null;
				for (int p = 1; p <= pages; p++)
				{
					result = service.GetAvailables(page: p, take: 5).Result;
					Assert.Equal(expected.Length, result.Total);
					Assert.Equal(p, result.Page);
					Assert.Equal(5, result.Players.Length);
					finalList.AddRange(result.Players.Select(ts => ts.Player));
				}
				result = service.GetAvailables(page: pages + 1, take: 5).Result;
				Assert.Equal(expected.Length, result.Total);
				Assert.Equal(pages + 1, result.Page);
				Assert.Equal(LastPage, result.Players.Length);
				finalList.AddRange(result.Players.Select(ts => ts.Player));
				Assert.Equal(expected.Length, finalList.Count);
				Assert.All(finalList, item => expected.Contains(item));
			});
		}
		[Fact]
		public void Get_PlayerList_WithTerms()
		{
			Fixture.RunInTransaction(context =>
			{
				var result = new PlayerService(null, new UnitOfWork(context, null))
					.GetAvailables("player").Result;
				var expected = SeedData.Players.Take(4);
				Assert.True(result.Total == 4);
				Assert.True(result.Terms == "player");
				Assert.True(result.Players.Length == expected.Count());
				Assert.All(result.Players.Select(ps => ps.Player), item => expected.Contains(item));
			});
		}
		[Fact]
		public void Get_PlayerList_NotInChampionship()
		{
			PlayerAvailablePaginationDTO result = null;
			var expected = SeedData.Players;
			string championship = "c1";
			Fixture.RunInTransaction(context =>
				{
					var prayers = new PlayerRepository(context).GetAll().Result;
					_output.WriteLine(JsonConvert.SerializeObject(prayers, Formatting.Indented));
					result = new PlayerService(null, new UnitOfWork(context, null))
						.GetAvailables(championship: championship).Result;
					Assert.Equal(5, result.Total);
					Assert.Equal(result.ExcludeFromChampionship, championship);
					Assert.Equal(5, result.Players.Length);
					Assert.All(result.Players.Select(PlayerService => PlayerService.Player),
						 item => expected.Contains(item));
				});
		}
	}
}
