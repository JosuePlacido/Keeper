
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
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Keeper.Application.Contract;
using Keeper.Application.Services.EditChampionship;

namespace Keeper.Test.Integration.Application
{
	public class TestSingleStatistic : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		private readonly IMapper _mapper;
		public SharedDatabaseFixture Fixture { get; }
		private readonly ApplicationContext _context;
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

			_context = Fixture.CreateContext();
		}

		[Fact]
		public void Get_Statistic_Of_Teams()
		{
			TeamStatisticDTO[] expected = _mapper.Map<TeamStatisticDTO[]>(SeedData.TeamsSubscribes);
			TeamStatisticDTO[] result;
			result = new EditChampionshipService(_mapper, new UnitOfWork(_context))
				.TeamStats("c1").Result;
			Assert.All(result, r => expected.Contains(r));
		}
		[Fact]
		public void Get_Statistic_Of_Teams_Empty()
		{
			TeamStatisticDTO[] result;

			{
				result = new EditChampionshipService(_mapper, new UnitOfWork(_context))
					.TeamStats("noexist").Result;
			}
			Assert.Empty(result);
		}
		[Fact]
		public void Get_Statistic_Of_Players()
		{
			PlayerStatisticDTO[] expected = _mapper.Map<PlayerStatisticDTO[]>(SeedData.PlayersSubscribe);
			PlayerStatisticDTO[] result;

			{
				result = new EditChampionshipService(_mapper, new UnitOfWork(_context))
					.PlayerStats("c1").Result;
			}
			Assert.All(result, r => expected.Contains(r));
		}
		[Fact]
		public void Get_Statistic_Of_Players_Empty()
		{
			PlayerStatisticDTO[] result;

			{
				result = new EditChampionshipService(_mapper, new UnitOfWork(_context))
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

			{
				var result = new EditChampionshipService(_mapper, new UnitOfWork(_context))
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
			{
				var result = new EditChampionshipService(_mapper, new UnitOfWork(_context))
					.UpdateTeamsStatistics(new TeamSubscribePost[] { test }).Result;
				Assert.NotNull(result);
			}
		}
	}
}
