using AutoMapper;
using Domain.Models;
using Infrastructure.CrossCutting.Adapter;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.Data;
using Application.Services.CRUDTeam;
using Application.DTO;

namespace Test.Integration.Application.CRUDTeamService;
public class GetsAndListTests : IClassFixture<SharedDatabaseFixture>
{
	public SharedDatabaseFixture Fixture { get; }
	public GetsAndListTests(SharedDatabaseFixture fixture, ITestOutputHelper output)
		=> Fixture = fixture;

	[Fact]
	public void GetTeam_Success()
	{
		Fixture.RunInTransaction(context =>
		{
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<TeamDTOProfile>();
			});
			IMapper mapper = config.CreateMapper();
			Team test = SeedData.Teams[0];
			Team result = new TeamService(mapper, new UnitOfWork(context, null))
				.Get(test.Id).Result;
			Assert.NotNull(result);
			Assert.Equal(test, result);
		});
	}

	[Fact]
	public void GetTeam_NoExist_Success()
	{
		Fixture.RunInTransaction(context =>
		{
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<TeamDTOProfile>();
			});
			IMapper mapper = config.CreateMapper();
			Team result = new TeamService(mapper, new UnitOfWork(context, null))
				.Get("no one").Result;
			Assert.Null(result);
		});
	}
	[Fact]
	public void ListTeams_Success()
	{
		Fixture.RunInTransaction(context =>
		{
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<TeamDTOProfile>();
			});
			IMapper mapper = config.CreateMapper();
			PaginationDTO<Team> result = new TeamService(mapper, new UnitOfWork(context, null))
				.List("", 1, 10).Result;
			Assert.Equal(7, result.Total);
			Assert.NotEmpty(result.Items);
		});
	}
	[Fact]
	public void ListTeams_Availables_Success()
	{
		Fixture.RunInTransaction(context =>
		{
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<TeamDTOProfile>();
			});
			IMapper mapper = config.CreateMapper();
			PaginationDTO<Team> result = new TeamService(mapper, new UnitOfWork(context, null))
				.GetTeamsAvailablesForChampionship("", "", 1, 10).Result;
			Assert.Equal(7, result.Total);
			Assert.NotEmpty(result.Items);
		});
	}
}

