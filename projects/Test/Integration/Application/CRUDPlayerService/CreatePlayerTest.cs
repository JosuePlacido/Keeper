using AutoMapper;
using Domain.Models;
using Infrastructure.CrossCutting.Adapter;
using Test.DataExamples;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.Data;
using Application.Services.CRUDPlayer;

namespace Test.Integration.Application.CRUDPlayerService;
public class CreatePlayerTest : IClassFixture<SharedDatabaseFixture>
{
	public SharedDatabaseFixture Fixture { get; }
	public CreatePlayerTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
		=> Fixture = fixture;

	[Fact]
	public void TestCreatePlayer_Success()
	{
		Player result = null;
		Fixture.RunInTransaction(context =>
			{
				var config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<PlayerDTOProfile>();
				});
				var mapper = config.CreateMapper();
				var test = PlayerDTODataExample.PlayerFull;
				result = new PlayerService(mapper, new UnitOfWork(context, null))
					.Create(test).Result;
				Assert.NotNull(result.Id);
				Assert.NotNull(context.Players.Find(result.Id));
			}
		);
	}
}

