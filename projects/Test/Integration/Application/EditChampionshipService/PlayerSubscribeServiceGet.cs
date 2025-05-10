
using System.Linq;
using AutoMapper;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.Repository;
using Infrastructure.CrossCutting.Adapter;
using Infrastructure.Data;
using Application.Services.EditChampionship;

namespace Test.Integration.Application
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

			Fixture.RunInTransaction(context =>
			{
				ChampionshipRepository repo = new ChampionshipRepository(context);
				MapperConfiguration config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<SquadEditDTOProfile>();
				});
				IMapper mapper = config.CreateMapper();
				string champ = repo.GetAll().Result.Where(c => c.Edition == "1993")
					.FirstOrDefault().Id;
				result = new EditChampionshipService(mapper, new UnitOfWork(context, null))
					.GetSquads(champ).Result;
				Assert.Equal(2, result.Length);
				Assert.Equal(6, result.SelectMany(ts => ts.Players).Count());
			});
		}
		[Fact]
		public void Get_PlayerListINvalidChampionchip_ReturnErrors()
		{
			Fixture.RunInTransaction(context =>
			{
				SquadEditDTO[] result = new EditChampionshipService(null, new UnitOfWork(context, null))
					.GetSquads("player").Result;
				Assert.Empty(result);
			});
		}
	}
}
