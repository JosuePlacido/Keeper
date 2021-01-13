using System.Linq;
using Application.Services;
using AutoMapper;
using Domain.Models;
using Infrastructure.CrossCutting.Adapter;
using Infrastructure.CrossCutting.DTO;
using Infrastructure.Repository;
using Test.DataExamples;
using Xunit;
using Xunit.Abstractions;

namespace Test.Integration.Application
{
	public class CRUDTeamTest : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		public SharedDatabaseFixture Fixture { get; }
		public CRUDTeamTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (_output, Fixture) = (output, fixture);

		[Fact]
		public void TestCreateChampionship()
		{
			Team result = null;
			using (var context = Fixture.CreateContext())
			{
				var repo = new TeamRepository(context);
				var config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<TeamDTOProfile>();
				});
				var mapper = config.CreateMapper();
				var test = TeamDTODataExample.TeamFull;
				result = new TeamService(mapper, repo).Create(test).Result;
				Assert.NotNull(result.Id);
				Assert.NotNull(context.Teams.Find(result.Id));
			}
		}
		[Fact]
		public void UpdateTeam()
		{
			using (var context = Fixture.CreateContext())
			{
				TeamUpdateDTO test = TeamDTODataExample.TeamUpdateNameOnly;
				test.Id = SeedData.Teams[4].Id;

				TeamRepository repo = new TeamRepository(context);
				MapperConfiguration config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<TeamDTOProfile>();
				});
				IMapper mapper = config.CreateMapper();
				Team result = new TeamService(mapper, repo).Update(test).Result;
				Team finalResult = context.Teams.Find(result.Id);
				Assert.NotNull(result.Id);
				Assert.NotNull(finalResult);
			}
		}
		[Fact]
		public void DeleteTeam()
		{
			Team result = null;
			using (var context = Fixture.CreateContext())
			{
				TeamRepository repo = new TeamRepository(context);
				Team test = SeedData.Teams.Last();
				result = new TeamService(null, repo).Delete(test).Result;
				Assert.Equal(result, test);
				Assert.Null(context.Teams.Find(result.Id));
			}
		}
		[Fact]
		public void GetTeam()
		{
			using (var context = Fixture.CreateContext())
			{
				TeamRepository repo = new TeamRepository(context);
				MapperConfiguration config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<TeamDTOProfile>();
				});
				IMapper mapper = config.CreateMapper();
				Team test = SeedData.Teams[0];
				Team result = new TeamService(mapper, repo).Get(test.Id).Result;
				Assert.NotNull(result);
				Assert.Equal(test, result);
			}
		}
		[Fact]
		public void ListTeam()
		{
			using (var context = Fixture.CreateContext())
			{
				TeamRepository repo = new TeamRepository(context);
				MapperConfiguration config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<TeamDTOProfile>();
				});
				IMapper mapper = config.CreateMapper();
				Team[] result = new TeamService(mapper, repo).List().Result;
				Assert.NotEmpty(result);
			}
		}
	}
}
