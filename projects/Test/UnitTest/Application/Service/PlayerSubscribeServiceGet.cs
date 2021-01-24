
using System.Linq;
using Keeper.Application.Services;
using AutoMapper;
using Xunit;
using Xunit.Abstractions;
using Keeper.Infrastructure.Repository;
using Keeper.Infrastructure.DAO;
using System.Collections.Generic;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Keeper.Infrastructure.CrossCutting.Adapter;
using Newtonsoft.Json;

namespace Keeper.Test.UnitTest.Application.Service
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
				SquadEditDTO[] result = new ChampionshipService(mapper, repo).GetSquads(champ).Result;
				Assert.Equal(2, result.Length);
				Assert.Equal(6, result.SelectMany(ts => ts.Players).Count());
			}
		}
		[Fact]
		public void Get_PlayerListINvalidChampionchip_ReturnErrors()
		{
			using (var context = Fixture.CreateContext())
			{
				ChampionshipRepository repo = new ChampionshipRepository(context);
				SquadEditDTO[] result = new ChampionshipService(null, repo)
					.GetSquads("player").Result;
				Assert.Empty(result);
			}
		}
	}
}
