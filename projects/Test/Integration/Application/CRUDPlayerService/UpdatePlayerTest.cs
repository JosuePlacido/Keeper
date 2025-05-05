using AutoMapper;
using Domain.Models;
using Infrastructure.CrossCutting.Adapter;
using Test.DataExamples;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.Data;
using Application.Services.CRUDPlayer;
using Application.DTO;

namespace Test.Integration.Application.CRUDPlayerService;
public class UpdatePlayerTest : IClassFixture<SharedDatabaseFixture>
{
	public SharedDatabaseFixture Fixture { get; }
	public UpdatePlayerTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
		=> Fixture = fixture;

	[Fact]
	public void UpdatePlayer_Success()
	{
		Fixture.RunInTransaction(context =>
		{
			PlayerUpdateDTO test = PlayerDTODataExample.PlayerUpdateNameOnly;
			test.Id = SeedData.Players[4].Id;
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<PlayerDTOProfile>();
			});
			IMapper mapper = config.CreateMapper();
			var result = new PlayerService(mapper, new UnitOfWork(context, null))
				.Update(test).Result;
			Player finalResult = context.Players.Find(result.Id);
			Assert.NotNull(result.Id);
			Assert.NotNull(finalResult);
		});
	}
	[Fact]
	public void UpdatePlayer_Fail_PLayerNotFound()
	{
		Fixture.RunInTransaction(async context =>
		{
			PlayerUpdateDTO test = PlayerDTODataExample.PlayerUpdateNameOnly;
			test.Id = "no exist id";
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<PlayerDTOProfile>();
			});
			IMapper mapper = config.CreateMapper();
			ValidationException ex = await Assert.ThrowsAsync<ValidationException>(() =>
				new PlayerService(mapper, new UnitOfWork(context, null))
				.Update(test));
			Assert.Equal("Falha ao alterar jogador", ex.Message);
			Assert.Equal("Id", ex.Errors[0].PropertyName);
			Assert.Equal("Jogador não encontrado", ex.Errors[0].ErrorMessage);
		});
	}
}

