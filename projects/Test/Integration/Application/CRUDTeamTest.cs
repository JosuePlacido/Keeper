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
using Keeper.Application.Interface;

namespace Keeper.Test.Integration.Application
{
	public class CRUDTeamTest : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		public SharedDatabaseFixture Fixture { get; }
		public CRUDTeamTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (_output, Fixture) = (output, fixture);

		[Fact]
		public void TestCreateTeam()
		{
			Team result = null;
			using (var transaction = Fixture.Connection.BeginTransaction())
			{
				using (var context = Fixture.CreateContext(transaction))
				{
					var repo = new TeamRepository(context);
					var config = new MapperConfiguration(cfg =>
					{
						cfg.AddProfile<TeamDTOProfile>();
					});
					var mapper = config.CreateMapper();
					var test = TeamDTODataExample.TeamFull;
					result = new TeamService(mapper, new UnitOfWork(context), repo, null).Create(test).Result;
					Assert.NotNull(result.Id);
					Assert.NotNull(context.Teams.Find(result.Id));
				}
			}
		}
		[Fact]
		public void UpdateTeam()
		{
			Team finalResult;
			IServiceResult result;
			using (var transaction = Fixture.Connection.BeginTransaction())
			{
				using (var context = Fixture.CreateContext(transaction))
				{
					TeamUpdateDTO test = TeamDTODataExample.TeamUpdateNameOnly;
					test.Id = SeedData.Teams[4].Id;

					TeamRepository repo = new TeamRepository(context);
					MapperConfiguration config = new MapperConfiguration(cfg =>
					{
						cfg.AddProfile<TeamDTOProfile>();
					});
					IMapper mapper = config.CreateMapper();
					result = new TeamService(mapper, new UnitOfWork(context),
						repo, new DAOTeam(context)).Update(test).Result;
					finalResult = context.Teams.Find(((Team)result.Value).Id);
				}
			}
			Assert.NotNull(((Team)result.Value).Id);
			Assert.NotNull(finalResult);
		}
		[Fact]
		public void DeleteTeam()
		{
			Team team;
			Team test;
			using (var transaction = Fixture.Connection.BeginTransaction())
			{
				using (var context = Fixture.CreateContext(transaction))
				{
					TeamRepository repo = new TeamRepository(context);
					MapperConfiguration config = new MapperConfiguration(cfg =>
					{
						cfg.AddProfile<TeamDTOProfile>();
					});
					IMapper mapper = config.CreateMapper();
					test = SeedData.Teams.Last();
					var result = new TeamService(mapper, new UnitOfWork(context),
						repo, new DAOTeam(context)).Delete(test.Id).Result;
					team = result.Value as Team;
					Assert.Null(context.Teams.Find(team.Id));
				}
			}
			Assert.IsType<Team>(team);
			Assert.Equal(team, test);
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
				Team result = new TeamService(mapper, new UnitOfWork(context),
					repo, new DAOTeam(context)).Get(test.Id).Result;
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
				Team[] result = new TeamService(mapper, new UnitOfWork(context),
					repo, new DAOTeam(context)).List().Result;
				Assert.NotEmpty(result);
			}
		}
	}
}
