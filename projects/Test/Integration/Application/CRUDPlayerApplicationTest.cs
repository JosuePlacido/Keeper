using System.Linq;
using Keeper.Application.Services;
using AutoMapper;
using Keeper.Domain.Models;
using Keeper.Infrastructure.CrossCutting.Adapter;
using Keeper.Application.DTO;
using Test.DataExamples;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.Data;

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
				using (var transaction = context.Database.BeginTransaction())
				{
					var config = new MapperConfiguration(cfg =>
					{
						cfg.AddProfile<PlayerDTOProfile>();
					});
					var mapper = config.CreateMapper();
					var test = PlayerDTODataExample.PlayerFull;
					result = new PlayerService(mapper, new UnitOfWork(context))
						.Create(test).Result;
					Assert.NotNull(result.Id);
					Assert.NotNull(context.Players.Find(result.Id));

					transaction.Rollback();
				}
			}
		}
		[Fact]
		public void UpdatePlayer()
		{
			using (var context = Fixture.CreateContext())
			{
				using (var transaction = context.Database.BeginTransaction())
				{
					PlayerUpdateDTO test = PlayerDTODataExample.PlayerUpdateNameOnly;
					test.Id = SeedData.Players[4].Id;
					MapperConfiguration config = new MapperConfiguration(cfg =>
					{
						cfg.AddProfile<PlayerDTOProfile>();
					});
					IMapper mapper = config.CreateMapper();
					var result = new PlayerService(mapper, new UnitOfWork(context))
						.Update(test).Result;
					Player finalResult = context.Players.Find(((Player)result.Value).Id);
					Assert.NotNull(((Player)result.Value).Id);
					Assert.NotNull(finalResult);

					transaction.Rollback();
				}
			}
		}
		[Fact]
		public void DeletePlayer()
		{
			using (var context = Fixture.CreateContext())
			{
				using (var transaction = context.Database.BeginTransaction())
				{
					MapperConfiguration config = new MapperConfiguration(cfg =>
					{
						cfg.AddProfile<PlayerDTOProfile>();
					});
					IMapper mapper = config.CreateMapper();
					Player test = SeedData.Players.Last();
					var result = new PlayerService(mapper, new UnitOfWork(context))
						.Delete(test.Id).Result;
					Player layer = result.Value as Player;
					Assert.IsType<Player>(layer);
					Assert.Equal(layer, test);
					Assert.Null(context.Players.Find(layer.Id));
					transaction.Rollback();
				}
			}
		}
		[Fact]
		public void GetPlayer()
		{
			using (var context = Fixture.CreateContext())
			{
				MapperConfiguration config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<PlayerDTOProfile>();
				});
				IMapper mapper = config.CreateMapper();
				Player test = SeedData.Players[0];
				Player result = new PlayerService(mapper, new UnitOfWork(context))
					.Get(test.Id).Result;
				Assert.NotNull(result);
				Assert.Equal(test, result);
			}
		}
		[Fact]
		public void ListPlayer()
		{
			using (var context = Fixture.CreateContext())
			{
				MapperConfiguration config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<PlayerDTOProfile>();
				});
				IMapper mapper = config.CreateMapper();
				PlayerSubscribe[] result = new PlayerService(mapper, new UnitOfWork(context))
					.GetAvailables().Result.Players;
				Assert.NotEmpty(result);
			}
		}
	}
}
