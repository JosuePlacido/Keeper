using System.Linq;
using AutoMapper;
using Domain.Models;
using Infrastructure.CrossCutting.Adapter;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.Data;
using Application.Services.CRUDPlayer;
using Application.DTO;

namespace Test.Integration.Application.CRUDPlayerService;
public class DeletePlayerTest : IClassFixture<SharedDatabaseFixture>
{
	public SharedDatabaseFixture Fixture { get; }
	public DeletePlayerTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
		=> Fixture = fixture;

	[Fact]
	public void DeletePlayer_Success()
	{
		Fixture.RunInTransaction(context =>
		{
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<PlayerDTOProfile>();
			});
			IMapper mapper = config.CreateMapper();
			Player test = SeedData.Players.Last();
			var result = new PlayerService(mapper, new UnitOfWork(context, null))
				.Delete(test.Id).Result;
			Player layer = result;
			Assert.IsType<Player>(layer);
			Assert.Equal(layer, test);
			Assert.Null(context.Players.Find(layer.Id));
		});
	}
	[Fact]
	public void DeletePlayer_Fail_PlayerDoesNotExist()
	{
		Fixture.RunInTransaction(async context =>
		{
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<PlayerDTOProfile>();
			});
			IMapper mapper = config.CreateMapper();
			ValidationException ex = await Assert.ThrowsAsync<ValidationException>(() =>
				new PlayerService(mapper, new UnitOfWork(context, null))
				.Delete("not found id"));
			Assert.Equal("Falha ao excluir jogador", ex.Message);
			Assert.Equal("Id", ex.Errors[0].PropertyName);
			Assert.Equal("Jogador não encontrado", ex.Errors[0].ErrorMessage);
		});
	}
	[Fact]
	public void DeletePlayer_Fail_PlayerSubscribed()
	{
		Fixture.RunInTransaction(async context =>
		{
			;
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<PlayerDTOProfile>();
			});
			IMapper mapper = config.CreateMapper();
			ValidationException ex = await Assert.ThrowsAsync<ValidationException>(() =>
				new PlayerService(mapper, new UnitOfWork(context, null))
				.Delete(SeedData.Players[4].Id));
			Assert.Equal("Falha ao excluir jogador", ex.Message);
			Assert.Equal("Id", ex.Errors[0].PropertyName);
			Assert.Equal("Jogador inscrito em campeonato", ex.Errors[0].ErrorMessage);
		});
	}
}

