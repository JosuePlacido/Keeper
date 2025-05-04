using System.Linq;
using AutoMapper;
using Domain.Models;
using Infrastructure.CrossCutting.Adapter;
using Test.DataExamples;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.Data;
using Application.Services.CRUDPlayer;

namespace Test.Integration.Application
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
			using (var transaction = Fixture.Connection.BeginTransaction())
			{
				using (var context = Fixture.CreateContext(transaction))
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

					transaction.Rollback();
				}
			}
		}
		[Fact]
		public void UpdatePlayer()
		{
			using (var transaction = Fixture.Connection.BeginTransaction())
			{
				using (var context = Fixture.CreateContext(transaction))
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
			using (var transaction = Fixture.Connection.BeginTransaction())
			{
				using (var context = Fixture.CreateContext(transaction))
				{
					MapperConfiguration config = new MapperConfiguration(cfg =>
					{
						cfg.AddProfile<PlayerDTOProfile>();
					});
					IMapper mapper = config.CreateMapper();
					Player test = SeedData.Players.Last();
					var result = new PlayerService(mapper, new UnitOfWork(context, null))
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
		public void ListPlayer()
		{
			Fixture.RunInTransaction(context =>
			{
				MapperConfiguration config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<PlayerDTOProfile>();
				});
				IMapper mapper = config.CreateMapper();
				PlayerSubscribe[] result = new PlayerService(mapper, new UnitOfWork(context, null))
					.GetAvailables().Result.Players;
				Assert.NotEmpty(result);
			});
		}
	}
}
