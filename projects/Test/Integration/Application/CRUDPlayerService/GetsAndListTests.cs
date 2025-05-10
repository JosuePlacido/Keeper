using AutoMapper;
using Domain.Models;
using Infrastructure.CrossCutting.Adapter;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.Data;
using Application.Services.CRUDPlayer;
using Application.DTO;

namespace Test.Integration.Application.CRUDPlayerService;
public class GetsAndListTests : IClassFixture<SharedDatabaseFixture>
{
	public SharedDatabaseFixture Fixture { get; }
	public GetsAndListTests(SharedDatabaseFixture fixture, ITestOutputHelper output)
		=> Fixture = fixture;

	[Fact]
	public void GetPlayer_Success()
	{
		Fixture.RunInTransaction(context =>
		{
			{
				MapperConfiguration config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<PlayerDTOProfile>();
				});
				IMapper mapper = config.CreateMapper();
				Player test = SeedData.Players[0];
				Player result = new PlayerService(mapper, new UnitOfWork(context, null))
					.Get(test.Id).Result;
				Assert.NotNull(result);
				Assert.Equal(test, result);
			}
		});
	}

	[Fact]
	public void GetPlayer_NoExist_Success()
	{
		Fixture.RunInTransaction(context =>
		{
			{
				MapperConfiguration config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<PlayerDTOProfile>();
				});
				IMapper mapper = config.CreateMapper();
				Player result = new PlayerService(mapper, new UnitOfWork(context, null))
					.Get("no one").Result;
				Assert.Null(result);
			}
		});
	}
	[Fact]
	public void ListPlayers_Success()
	{
		Fixture.RunInTransaction(context =>
		{
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<PlayerDTOProfile>();
			});
			IMapper mapper = config.CreateMapper();
			PaginationDTO<Player> result = new PlayerService(mapper, new UnitOfWork(context, null))
				.GetAvailables().Result;
			Assert.Equal(11, result.Total);
			Assert.NotEmpty(result.Items);
		});
	}
}

