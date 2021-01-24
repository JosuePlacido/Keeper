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

namespace Keeper.Test.Integration.Application
{
	public class CRUDPlayerTest : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		public SharedDatabaseFixture Fixture { get; }
		public CRUDPlayerTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (_output, Fixture) = (output, fixture);

		[Fact]
		public void TestCreatePlayer()
		{
			Player result = null;
			using (var context = Fixture.CreateContext())
			{
				var repo = new PlayerRepository(context);
				var config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<PlayerDTOProfile>();
				});
				var mapper = config.CreateMapper();
				var test = PlayerDTODataExample.PlayerFull;
				result = new PlayerService(mapper, repo, null).Create(test).Result;
				Assert.NotNull(result.Id);
				Assert.NotNull(context.Players.Find(result.Id));
			}
		}
		[Fact]
		public void UpdatePlayer()
		{
			using (var context = Fixture.CreateContext())
			{
				PlayerUpdateDTO test = PlayerDTODataExample.PlayerUpdateNameOnly;
				test.Id = SeedData.Players[4].Id;

				PlayerRepository repo = new PlayerRepository(context);
				MapperConfiguration config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<PlayerDTOProfile>();
				});
				IMapper mapper = config.CreateMapper();
				var result = new PlayerService(mapper, repo, new DAOPlayer(context)).Update(test).Result;
				Player finalResult = context.Players.Find(((Player)result.Value).Id);
				Assert.NotNull(((Player)result.Value).Id);
				Assert.NotNull(finalResult);
			}
		}
		[Fact]
		public void DeletePlayer()
		{
			using (var context = Fixture.CreateContext())
			{
				PlayerRepository repo = new PlayerRepository(context);
				MapperConfiguration config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<PlayerDTOProfile>();
				});
				IMapper mapper = config.CreateMapper();
				Player test = SeedData.Players.Last();
				var result = new PlayerService(mapper, repo, new DAOPlayer(context)).Delete(test.Id).Result;
				Player layer = result.Value as Player;
				Assert.IsType<Player>(layer);
				Assert.Equal(layer, test);
				Assert.Null(context.Players.Find(layer.Id));
			}
		}
		[Fact]
		public void GetPlayer()
		{
			using (var context = Fixture.CreateContext())
			{
				PlayerRepository repo = new PlayerRepository(context);
				MapperConfiguration config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<PlayerDTOProfile>();
				});
				IMapper mapper = config.CreateMapper();
				Player test = SeedData.Players[0];
				Player result = new PlayerService(mapper, repo, new DAOPlayer(context)).Get(test.Id).Result;
				Assert.NotNull(result);
				Assert.Equal(test, result);
			}
		}
		[Fact]
		public void ListPlayer()
		{
			using (var context = Fixture.CreateContext())
			{
				PlayerRepository repo = new PlayerRepository(context);
				MapperConfiguration config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<PlayerDTOProfile>();
				});
				IMapper mapper = config.CreateMapper();
				Player[] result = new PlayerService(mapper, repo, new DAOPlayer(context))
					.GetAvailables().Result.Players;
				Assert.NotEmpty(result);
			}
		}
	}
}
