using System.Linq;
using Keeper.Application.Services;
using AutoMapper;
using Keeper.Domain.Models;
using Keeper.Infrastructure.CrossCutting.Adapter;
using Keeper.Application.DTO;
using Keeper.Infrastructure.Repository;
using Keeper.Test;
using Test.DataExamples;
using Xunit;
using Xunit.Abstractions;
using Keeper.Infrastructure.DAO;
using Infrastructure.Data;
using Keeper.Domain.Enum;
using Keeper.Application.Interface;

namespace Keeper.Test.Integration.Application
{
	public class TestScheduleMatch : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		private readonly IMapper _mapper;
		public SharedDatabaseFixture Fixture { get; }
		public TestScheduleMatch(SharedDatabaseFixture fixture, ITestOutputHelper output)
		{
			(_output, Fixture) = (output, fixture);

			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<MatchEditProfile>();
			});
			_mapper = config.CreateMapper();
		}
		[Fact]
		public void Get_Valid_Championship_Return_List()
		{
			Match[] expected = SeedData.Matches;
			MatchEditsScope result = null;
			using (var context = Fixture.CreateContext())
			{
				ChampionshipRepository repo = new ChampionshipRepository(context);
				result = new ChampionshipService(_mapper, new UnitOfWork(context), repo,
					new DAOPlayerSubscribe(context), null, null, null, null, null)
				.GetMatchSchedule("c1").Result;
			}
			Assert.Equal(expected.Select(m => m.Id),
				result.Stages[0].Groups[0].Matchs.Select(m => m.Id));
		}
		[Fact]
		public void Get_Valid_Championship_Return_Empty()
		{
			MatchEditsScope result = null;
			using (var context = Fixture.CreateContext())
			{
				ChampionshipRepository repo = new ChampionshipRepository(context);
				result = new ChampionshipService(_mapper, new UnitOfWork(context), repo,
					new DAOPlayerSubscribe(context), null, null, null, null, null)
				.GetMatchSchedule("noexist").Result;
			}
			Assert.Null(result);
		}
	}
}
