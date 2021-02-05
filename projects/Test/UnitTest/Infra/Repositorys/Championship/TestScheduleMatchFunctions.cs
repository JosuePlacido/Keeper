using System.Linq;
using Xunit;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Keeper.Infrastructure.Repository;
using Keeper.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Keeper.Test.UnitTest.Infra.Repositorys
{
	public class TestScheduleMatchFunctions : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public TestScheduleMatchFunctions(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);
		[Fact]
		public void GetMatchesSchedule()
		{
			Match[] expected = SeedData.Matches;
			Championship result = null;
			using (var context = Fixture.CreateContext())
			{
				result = new ChampionshipRepository(context)
					.GetByIdWithMatchWithTeams("c1").Result;
			}
			Assert.Equal(expected, result.Stages[0].Groups[0].Matchs);
		}
	}
}
