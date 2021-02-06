
using System.Linq;
using Keeper.Application.Services;
using AutoMapper;
using Xunit;
using Xunit.Abstractions;
using Keeper.Infrastructure.Repository;
using Keeper.Infrastructure.CrossCutting.Adapter;
using Keeper.Infrastructure.Data;
using Keeper.Application.Services.EditChampionship;

namespace Keeper.Test.Integration.Application
{
	public class PlayerSubscribeServiceList : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		public SharedDatabaseFixture Fixture { get; }
		public PlayerSubscribeServiceList(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (_output, Fixture) = (output, fixture);

		[Fact]
		public void Get_PlayerList_ReturnList()
		{
			SquadEditDTO[] result = null;
			using (var context = Fixture.CreateContext())
			{
				ChampionshipRepository repo = new ChampionshipRepository(context);
				MapperConfiguration config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<SquadEditDTOProfile>();
				});
				IMapper mapper = config.CreateMapper();
				string champ = repo.GetAll().Result.Where(c => c.Edition == "1993")
					.FirstOrDefault().Id;
				result = new EditChampionshipService(mapper, new UnitOfWork(context))
					.GetSquads(champ).Result;
			}
			Assert.Equal(2, result.Length);
			Assert.Equal(6, result.SelectMany(ts => ts.Players).Count());
		}
		[Fact]
		public void Get_PlayerListINvalidChampionchip_ReturnErrors()
		{
			using (var context = Fixture.CreateContext())
			{
				SquadEditDTO[] result = new EditChampionshipService(null, new UnitOfWork(context))
					.GetSquads("player").Result;
				Assert.Empty(result);
			}
		}
	}
}
