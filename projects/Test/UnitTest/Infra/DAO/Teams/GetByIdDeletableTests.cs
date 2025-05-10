using Xunit;
using Xunit.Abstractions;
using Infrastructure.DAO;
using Application.Contract.DAL;

namespace Test.UnitTest.Infra.DAO.Teams;

public class GetByIdDeletableTests : IClassFixture<SharedDatabaseFixture>
{
	public SharedDatabaseFixture Fixture { get; }
	private readonly ITestOutputHelper _output;
	public GetByIdDeletableTests(SharedDatabaseFixture fixture, ITestOutputHelper output)
		=> (Fixture, _output) = (fixture, output);

	[Fact]
	public void Delete_ExistingTeam_Success()
	{
		Fixture.RunInTransaction(context =>
		{
			IDAOTeam dao = new DAOTeam(context);
			var (Team, isDeletable) = dao.GetByIdDeletable("t1").Result;
			Assert.True(isDeletable);
			Assert.Equal(Team, SeedData.Teams[0]);

		});
	}
	[Fact]
	public void Delete_NoExistTeam_ThrowException()
	{
		Fixture.RunInTransaction(context =>
		{
			IDAOTeam dao = new DAOTeam(context);
			var (Team, isDeletable) = dao.GetByIdDeletable("no exist").Result;

			Assert.False(isDeletable);
			Assert.Null(Team);
		});
	}
	[Fact]
	public void Delete_TeamRegistered_ThrowException()
	{
		Fixture.RunInTransaction(context =>
		{
			IDAOTeam dao = new DAOTeam(context);
			var (Team, isDeletable) = dao.GetByIdDeletable("t5").Result;

			Assert.False(isDeletable);
			Assert.Equal(Team, SeedData.Teams[4]);
		});
	}
}
