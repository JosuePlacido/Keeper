using System.Linq;
using Infrastructure.DAO;
using Xunit;
using Xunit.Abstractions;

namespace Test.Infra.DAO
{
	public class TestVerifyIds : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public TestVerifyIds(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);
		[Fact]
		public void VeryfiValidIdOfGroup()
		{
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOGroup(context);
				var idsInvalids = dao.VerifyId(new string[] { "inedito" });
				Assert.Empty(idsInvalids);
			}
		}
		[Fact]
		public void VeryfiInvalidIdOfGroup()
		{
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOGroup(context);
				var ids = dao.GetAll().Result.OrderBy(g => g.Id).Take(3)
					.Select(g => g.Id).ToArray();
				var idsInvalids = dao.VerifyId(ids);
				Assert.NotEmpty(idsInvalids);
				Assert.Equal(ids, idsInvalids);
			}
		}
		[Fact]
		public void VeryfiValidIdOfVacancy()
		{
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOVacancy(context);
				var idsInvalids = dao.VerifyId(new string[] { "inedito" });
				Assert.Empty(idsInvalids);
			}
		}
		[Fact]
		public void VeryfiInvalidIdOfVacancy()
		{
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOVacancy(context);
				var ids = dao.GetAll().Result.OrderBy(g => g.Id).Take(3)
					.Select(g => g.Id).ToArray();
				var idsInvalids = dao.VerifyId(ids);
				Assert.NotEmpty(idsInvalids);
				Assert.Equal(ids, idsInvalids);
			}
		}
		[Fact]
		public void VeryfiValidIdOfPlayerSubscribe()
		{
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOPlayerSubscribe(context);
				var idsInvalids = dao.VerifyId(new string[] { "inedito" });
				Assert.Empty(idsInvalids);
			}
		}
		[Fact]
		public void VeryfiInvalidIdOfPlayerSubscribe()
		{
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOPlayerSubscribe(context);
				var ids = dao.GetAll().Result.OrderBy(g => g.Id).Take(3)
					.Select(g => g.Id).ToArray();
				var idsInvalids = dao.VerifyId(ids);
				Assert.NotEmpty(idsInvalids);
				Assert.Equal(ids, idsInvalids);
			}
		}
		[Fact]
		public void VeryfiValidIdOfTeamSubscribe()
		{
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOTeamSubscribe(context);
				var idsInvalids = dao.VerifyId(new string[] { "inedito" });
				Assert.Empty(idsInvalids);
			}
		}
		[Fact]
		public void VeryfiInvalidIdOfTeamSubscribe()
		{
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOTeamSubscribe(context);
				var ids = dao.GetAll().Result.OrderBy(g => g.Id).Take(3)
					.Select(g => g.Id).ToArray();
				var idsInvalids = dao.VerifyId(ids);
				Assert.NotEmpty(idsInvalids);
				Assert.Equal(ids, idsInvalids);
			}
		}
	}
}
