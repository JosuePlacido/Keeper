using AutoMapper;
using Domain.Models;
using Infrastructure.CrossCutting.Adapter;
using Test.DataExamples;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.Data;
using Application.Services.CRUDTeam;

namespace Test.Integration.Application.CRUDTeamService;
public class CreateTeamTest : IClassFixture<SharedDatabaseFixture>
{
	public SharedDatabaseFixture Fixture { get; }
	public CreateTeamTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
		=> Fixture = fixture;

	[Fact]
	public void TestCreateTeam_Success()
	{
		Team result = null;
		Fixture.RunInTransaction(context =>
			{
				var config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<TeamDTOProfile>();
				});
				var mapper = config.CreateMapper();
				var test = TeamDTODataExample.TeamFull;
				result = new TeamService(mapper, new UnitOfWork(context, null))
					.Create(test).Result;
				Assert.NotNull(result.Id);
				Assert.NotNull(context.Teams.Find(result.Id));
			}
		);
	}
}

