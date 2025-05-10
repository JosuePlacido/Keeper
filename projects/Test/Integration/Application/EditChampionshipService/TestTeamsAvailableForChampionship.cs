
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.Repository;
using System.Collections.Generic;
using Domain.Models;
using Infrastructure.Data;
using Application.Services.CRUDTeam;
using Application.DTO;

namespace Test.Integration.Application
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
			PaginationDTO<Team> result = null;
			string champ = SeedData.Championship.Id;

			using (var transaction = Fixture.Connection.BeginTransaction())
			using (var context = Fixture.CreateContext(transaction))
			{
				result = new TeamService(null, new UnitOfWork(context, null))
					.GetTeamsAvailablesForChampionship("", champ, 1, 30).Result;
			}
			Assert.All(expected, item => Assert.DoesNotContain(item, result.Items));
		}
		[Fact]
		public void Get_TeamList_WithNullChampionship_ReturnAllTeams()
		{
			Team[] expected = SeedData.Teams.OrderBy(t => t.Name).ToArray();
			PaginationDTO<Team> result = null;

			using (var transaction = Fixture.Connection.BeginTransaction())
			using (var context = Fixture.CreateContext(transaction))
			{
				result = new TeamService(null, new UnitOfWork(context, null))
					.GetTeamsAvailablesForChampionship("", "", 1, 30).Result;
			}
			Assert.Equal(expected, result.Items);
		}
		[Fact]
		public void Get_TeamList_PageAndTakes()
		{
			Team[] expected = SeedData.Teams.OrderBy(t => t.Name).ToArray();
			int pages = expected.Length / 2;
			int LastPage = expected.Length % 2;
			PaginationDTO<Team> result = null;
			List<Team> finalList = new List<Team>();

			using (var transaction = Fixture.Connection.BeginTransaction())
			using (var context = Fixture.CreateContext(transaction))
			{
				var service = new TeamService(null, new UnitOfWork(context, null));
				for (int p = 1; p <= pages; p++)
				{
					result = service.GetTeamsAvailablesForChampionship("", "", page: p, take: 2).Result;
					Assert.Equal(expected.Length, result.Total);
					Assert.Equal(p, result.Page);
					Assert.Equal(2, result.Items.Length);
					finalList.AddRange(result.Items);
				}
				result = service.GetTeamsAvailablesForChampionship("", "", page: pages + 1, take: 2).Result;
			}
			Assert.Equal(expected.Length, result.Total);
			Assert.Equal(pages + 1, result.Page);
			Assert.Equal(LastPage, result.Items.Length);
			finalList.AddRange(result.Items);
			Assert.Equal(expected.Length, finalList.Count);
			Assert.All(result.Items, item => expected.Contains(item));
			Assert.All(result.Items, item => expected.Contains(item));
		}
		[Fact]
		public void Get_TeamList_WithTerms()
		{

			using (var transaction = Fixture.Connection.BeginTransaction())
			using (var context = Fixture.CreateContext(transaction))
			{
				TeamRepository repo = new TeamRepository(context);
				var result = new TeamService(null, new UnitOfWork(context, null))
					.GetTeamsAvailablesForChampionship("TImÉ", "", 1, 10).Result;
				var expected = SeedData.Teams.Take(4);
				Assert.Equal(expected.Count(), result.Total);
				Assert.Equal(expected.Count(), result.Items.Length);
				Assert.All(result.Items, item => expected.Contains(item));
			}
		}
	}
}
