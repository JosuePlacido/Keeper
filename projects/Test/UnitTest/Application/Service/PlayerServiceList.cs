
using System.Linq;
using Keeper.Application.Services;
using AutoMapper;
using Xunit;
using Xunit.Abstractions;
using Keeper.Infrastructure.Repository;
using Keeper.Infrastructure.DAO;
using System.Collections.Generic;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Newtonsoft.Json;
using Infrastructure.Data;

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
			using (var context = Fixture.CreateContext())
			{
				PlayerRepository repo = new PlayerRepository(context);
				Player[] expected = repo.GetAll().Result;
				int pages = expected.Length / 5;
				int LastPage = expected.Length % 5;
				List<Player> finalList = new List<Player>();
				DAOPlayer dao = new DAOPlayer(context);
				var service = new PlayerService(null, new UnitOfWork(context), repo, dao);
				PlayerPaginationDTO result = null;
				for (int p = 1; p <= pages; p++)
				{
					result = service.GetAvailables(page: p, take: 5).Result;
					Assert.Equal(expected.Length, result.Total);
					Assert.Equal(p, result.Page);
					Assert.Equal(5, result.Players.Length);
					finalList.AddRange(result.Players);
				}
				result = service.GetAvailables(page: pages + 1, take: 5).Result;
				Assert.Equal(expected.Length, result.Total);
				Assert.Equal(pages + 1, result.Page);
				Assert.Equal(LastPage, result.Players.Length);
				finalList.AddRange(result.Players);
				Assert.Equal(expected.Length, finalList.Count);
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
				var result = new PlayerService(null, new UnitOfWork(context), repo, new DAOPlayer(context))
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
				string championship = new ChampionshipRepository(context).GetAll()
					.Result.Where(c => c.Edition == "1993")
					.FirstOrDefault().Id;
				var prayers = new PlayerRepository(context).GetAll().Result;
				_output.WriteLine(JsonConvert.SerializeObject(prayers, Formatting.Indented));
				PlayerRepository repo = new PlayerRepository(context);
				var result = new PlayerService(null, new UnitOfWork(context), repo, new DAOPlayer(context))
					.GetAvailables(championship: championship).Result;
				var expected = SeedData.Players;
				Assert.Equal(5, result.Total);
				Assert.Equal(result.ExcludeFromChampionship, championship);
				Assert.Equal(result.Players.Length, 5);
				Assert.All(result.Players, item => expected.Contains(item));
			}
		}
	}
}
