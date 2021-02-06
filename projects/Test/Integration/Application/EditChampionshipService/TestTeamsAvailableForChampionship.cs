
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
using  Keeper.Infrastructure.Data;

namespace Keeper.Test.Integration.Application
{
	public class TestTeamsAvailableForChampionship : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		public SharedDatabaseFixture Fixture { get; }
		public TestTeamsAvailableForChampionship(SharedDatabaseFixture fixture,
			ITestOutputHelper output) => (_output, Fixture) = (output, fixture);

		[Fact]
		public void Get_TeamList_NotInChampionship()
		{
			Team[] expected = SeedData.Teams.Skip(4).Take(2).ToArray();
			TeamPaginationDTO result = null;
			string champ = SeedData.Championship.Id;
			using (var context = Fixture.CreateContext())
			{
				result = new TeamService(null, new UnitOfWork(context))
					.GetTeamsAvailablesForChampionship("", champ, 1, 30).Result;
			}
			Assert.All(expected, item => Assert.DoesNotContain(item, result.Teams));
		}
		[Fact]
		public void Get_TeamList_WithNullChampionship_ReturnAllTeams()
		{
			Team[] expected = SeedData.Teams.OrderBy(t => t.Name).ToArray();
			TeamPaginationDTO result = null;
			using (var context = Fixture.CreateContext())
			{
				result = new TeamService(null, new UnitOfWork(context))
					.GetTeamsAvailablesForChampionship("", "", 1, 30).Result;
			}
			Assert.Equal(expected, result.Teams);
		}
		[Fact]
		public void Get_TeamList_PageAndTakes()
		{
			Team[] expected = SeedData.Teams.OrderBy(t => t.Name).ToArray();
			int pages = expected.Length / 2;
			int LastPage = expected.Length % 2;
			TeamPaginationDTO result = null;
			List<Team> finalList = new List<Team>();
			using (var context = Fixture.CreateContext())
			{
				var service = new TeamService(null, new UnitOfWork(context));
				for (int p = 1; p <= pages; p++)
				{
					result = service.GetTeamsAvailablesForChampionship("", "", page: p, take: 2).Result;
					Assert.Equal(expected.Length, result.Total);
					Assert.Equal(p, result.Page);
					Assert.Equal(2, result.Teams.Length);
					finalList.AddRange(result.Teams);
				}
				result = service.GetTeamsAvailablesForChampionship("", "", page: pages + 1, take: 2).Result;
			}
			Assert.Equal(expected.Length, result.Total);
			Assert.Equal(pages + 1, result.Page);
			Assert.Equal(LastPage, result.Teams.Length);
			finalList.AddRange(result.Teams);
			Assert.Equal(expected.Length, finalList.Count);
			Assert.All(result.Teams, item => expected.Contains(item));
			Assert.All(result.Teams, item => expected.Contains(item));
		}
		[Fact]
		public void Get_TeamList_WithTerms()
		{
			using (var context = Fixture.CreateContext())
			{
				TeamRepository repo = new TeamRepository(context);
				var result = new TeamService(null, new UnitOfWork(context))
					.GetTeamsAvailablesForChampionship("TImÉ", "", 1, 10).Result;
				var expected = SeedData.Teams.Take(4);
				Assert.Equal(expected.Count(), result.Total);
				Assert.Equal("TImÉ", result.Terms);
				Assert.Equal(expected.Count(), result.Teams.Length);
				Assert.All(result.Teams, item => expected.Contains(item));
			}
		}
	}
}
