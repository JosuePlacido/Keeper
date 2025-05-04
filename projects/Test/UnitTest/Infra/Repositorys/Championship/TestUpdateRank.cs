using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.Repository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Test.UnitTest.Infra.Repositorys
{
	public class TestUpdateRank : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public TestUpdateRank(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);

		[Fact]
		public void GetRankFromDAO()
		{
			Championship result;
			using (var context = Fixture.CreateContext())
			{
				var repo = new ChampionshipRepository(context);
				result = repo.GetByIdWithRank("c1").Result;
			}
			Assert.NotEmpty(result.Stages[0].Groups[0].Statistics);
		}

		[Fact]
		public void PostRank()
		{
			Statistic result;
			Statistic statistic = Statistic.Factory("s1", "ts1", games: 5);
			using (var context = Fixture.CreateContext())
			{
				var repo = new ChampionshipRepository(context);
				var temp = repo.UpdateStatistics(new Statistic[] { statistic });
				context.SaveChanges();
				result = context.Statistics.AsNoTracking().Where(s => s.Id == "s1")
					.FirstOrDefault();
			}
			Assert.Equal(5, result.Games);
		}
	}
}
