using System.Linq;
using AutoMapper;
using Domain.Models;
using Infrastructure.CrossCutting.Adapter;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.Data;
using Application.Services.CRUDTeam;
using Application.DTO;

namespace Test.Integration.Application.CRUDTeamService;
public class DeleteTeamTest : IClassFixture<SharedDatabaseFixture>
{
	public SharedDatabaseFixture Fixture { get; }
	public DeleteTeamTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
		=> Fixture = fixture;

	[Fact]
	public void DeleteTeam_Success()
	{
		Fixture.RunInTransaction(context =>
		{
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<TeamDTOProfile>();
			});
			IMapper mapper = config.CreateMapper();
			Team test = SeedData.Teams.Last();
			var result = new TeamService(mapper, new UnitOfWork(context, null))
				.Delete(test.Id).Result;
			Team layer = result;
			Assert.IsType<Team>(layer);
			Assert.Equal(layer, test);
			Assert.Null(context.Teams.Find(layer.Id));
		});
	}
	[Fact]
	public void DeleteTeam_Fail_TeamDoesNotExist()
	{
		Fixture.RunInTransaction(async context =>
		{
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<TeamDTOProfile>();
			});
			IMapper mapper = config.CreateMapper();
			ValidationException ex = await Assert.ThrowsAsync<ValidationException>(() =>
				new TeamService(mapper, new UnitOfWork(context, null))
				.Delete("not found id"));
			Assert.Equal("Falha ao excluir time", ex.Message);
			Assert.Equal("Id", ex.Errors[0].PropertyName);
			Assert.Equal("Time não encontrado", ex.Errors[0].ErrorMessage);
		});
	}
	[Fact]
	public void DeleteTeam_Fail_TeamSubscribed()
	{
		Fixture.RunInTransaction(async context =>
		{
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<TeamDTOProfile>();
			});
			IMapper mapper = config.CreateMapper();
			ValidationException ex = await Assert.ThrowsAsync<ValidationException>(() =>
				new TeamService(mapper, new UnitOfWork(context, null))
				.Delete(SeedData.Teams[4].Id));
			Assert.Equal("Falha ao excluir time", ex.Message);
			Assert.Equal("Id", ex.Errors[0].PropertyName);
			Assert.Equal("Time inscrito em campeonato", ex.Errors[0].ErrorMessage);
		});
	}
}

