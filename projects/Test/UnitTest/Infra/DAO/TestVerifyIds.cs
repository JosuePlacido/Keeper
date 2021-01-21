using System.Linq;
using Keeper.Domain.Models;
using Keeper.Infrastructure.DAO;
using Keeper.Test;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Keeper.Test.Infra.DAO
{
	public class TestVerifyIds : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public TestVerifyIds(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);
		[Fact]
		public void TestValidIds()
		{
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOChampionship(context);
				var idsInvalids = dao.VerifyCreatedIds(SeedData.GetChampionship());
				Assert.Empty(idsInvalids);
			}
		}
		[Fact]
		public void TestInvalidIds()
		{
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOChampionship(context);
				Championship championship = context.Championships.Where(chp =>
				chp.Name == "Recopa Sulamericana")
				.Include(chp => chp.Stages)
					.ThenInclude(stg => ((Stage)stg).Groups)
						.ThenInclude(grp => ((Group)grp).Vacancys)
				.Include(chp => chp.Teams)
						.ThenInclude(ts => ((TeamSubscribe)ts).Players)
				.FirstOrDefault();
				var idsInvalids = dao.VerifyCreatedIds(championship);
				Assert.NotEmpty(idsInvalids);
				Assert.True(idsInvalids.Length == 11);
			}
		}
	}
}
