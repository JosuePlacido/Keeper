
using System.Linq;
using AutoMapper;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.CrossCutting.Adapter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Services.EditChampionship;

namespace Test.Integration.Application
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
			Fixture.RunInTransaction(context =>
			{
				TeamStatisticDTO[] expected = _mapper.Map<TeamStatisticDTO[]>(SeedData.TeamsSubscribes);
				TeamStatisticDTO[] result;
				result = new EditChampionshipService(_mapper, new UnitOfWork(context, null))
					.TeamStats("c1").Result;
				Assert.All(result, r => expected.Contains(r));
			});
		}
		[Fact]
		public void Get_Statistic_Of_Teams_Empty()
		{
			Fixture.RunInTransaction(context =>
			{
				TeamStatisticDTO[] result;

				{
					result = new EditChampionshipService(_mapper, new UnitOfWork(context, null))
						.TeamStats("noexist").Result;
				}
				Assert.Empty(result);
			});
		}
		[Fact]
		public void Get_Statistic_Of_Players()
		{
			Fixture.RunInTransaction(context =>
			{
				PlayerStatisticDTO[] expected = _mapper.Map<PlayerStatisticDTO[]>(SeedData.PlayersSubscribe);
				PlayerStatisticDTO[] result;

				{
					result = new EditChampionshipService(_mapper, new UnitOfWork(context, null))
						.PlayerStats("c1").Result;
				}
				Assert.All(result, r => expected.Contains(r));
			});
		}
		[Fact]
		public void Get_Statistic_Of_Players_Empty()
		{
			Fixture.RunInTransaction(context =>
			{
				PlayerStatisticDTO[] result;
				{
					result = new EditChampionshipService(_mapper, new UnitOfWork(context, null))
						.PlayerStats("noexist").Result;
				}
				Assert.Empty(result);
			});
		}

		[Fact]
		public void Update_Player_Return_Ok()
		{
			Fixture.RunInTransaction(context =>
			{
				PlayerSubscribePost test = new PlayerSubscribePost
				{
					Id = "ps1",
					Games = 5
				};

				{
					var result = new EditChampionshipService(_mapper, new UnitOfWork(context, null))
						.UpdatePlayersStatistics(new PlayerSubscribePost[] { test }).Result;
					Assert.NotNull(result);
				}
			});
		}
		[Fact]
		public void Update_Team_Return_Ok()
		{
			Fixture.RunInTransaction(context =>
			{
				TeamSubscribePost test = new TeamSubscribePost
				{
					Id = "ts1",
					Games = 5
				};
				{
					var result = new EditChampionshipService(_mapper, new UnitOfWork(context, null))
						.UpdateTeamsStatistics(new TeamSubscribePost[] { test }).Result;
					Assert.NotNull(result);
				}
			});
		}
	}
}
