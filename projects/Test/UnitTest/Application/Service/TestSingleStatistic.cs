
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
using Keeper.Infrastructure.CrossCutting.Adapter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Keeper.Application.Interface;

namespace Keeper.Test.UnitTest.Application.Service
{
	public class TestSingleStatistic : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		private readonly IMapper _mapper;
		public SharedDatabaseFixture Fixture { get; }
		public TestSingleStatistic(SharedDatabaseFixture fixture,
			ITestOutputHelper output)
		{
			(_output, Fixture) = (output, fixture);
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<RankDTOProfile>();
				cfg.AddProfile<SingleStatisticDTOProfile>();
			});
			_mapper = config.CreateMapper();
		}

		[Fact]
		public void Get_Statistic_Of_Teams()
		{
			TeamStatisticDTO[] expected = _mapper.Map<TeamStatisticDTO[]>(SeedData.TeamsSubscribes);
			TeamStatisticDTO[] result;
			using (var context = Fixture.CreateContext())
			{
				result = new ChampionshipService(_mapper, new UnitOfWork(context))
					.TeamStats("c1").Result;
			}
			Assert.All(result, r => expected.Contains(r));
		}
		[Fact]
		public void Get_Statistic_Of_Teams_Empty()
		{
			TeamStatisticDTO[] result;
			using (var context = Fixture.CreateContext())
			{
				result = new ChampionshipService(_mapper, new UnitOfWork(context))
					.TeamStats("noexist").Result;
			}
			Assert.Empty(result);
		}
		[Fact]
		public void Get_Statistic_Of_Players()
		{
			PlayerStatisticDTO[] expected = _mapper.Map<PlayerStatisticDTO[]>(SeedData.PlayersSubscribe);
			PlayerStatisticDTO[] result;
			using (var context = Fixture.CreateContext())
			{
				result = new ChampionshipService(_mapper, new UnitOfWork(context))
					.PlayerStats("c1").Result;
			}
			Assert.All(result, r => expected.Contains(r));
		}
		[Fact]
		public void Get_Statistic_Of_Players_Empty()
		{
			PlayerStatisticDTO[] result;
			using (var context = Fixture.CreateContext())
			{
				result = new ChampionshipService(_mapper, new UnitOfWork(context))
					.PlayerStats("noexist").Result;
			}
			Assert.Empty(result);
		}

		[Fact]
		public void Update_Player_Return_Ok()
		{
			PlayerSubscribePost test = new PlayerSubscribePost
			{
				Id = "ps1",
				Games = 5
			};
			using (var context = Fixture.CreateContext())
			{
				var result = new ChampionshipService(_mapper, new UnitOfWork(context))
					.UpdatePlayersStatistics(new PlayerSubscribePost[] { test }).Result;
				Assert.NotNull(result);
			}
		}
		[Fact]
		public void Update_Team_Return_Ok()
		{
			TeamSubscribePost test = new TeamSubscribePost
			{
				Id = "ts1",
				Games = 5
			};
			using (var context = Fixture.CreateContext())
			{
				var result = new ChampionshipService(_mapper, new UnitOfWork(context))
					.UpdateTeamsStatistics(new TeamSubscribePost[] { test }).Result;
				Assert.NotNull(result);
			}
		}
	}
}
