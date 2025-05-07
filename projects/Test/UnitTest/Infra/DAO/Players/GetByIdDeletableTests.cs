using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Domain.Models;
using Infrastructure.DAO;
using Application.DTO;
using Application.Contract.DAL;

namespace Test.UnitTest.Infra.DAO.Players;

public class GetByIdDeletableTests : IClassFixture<SharedDatabaseFixture>
{
	public SharedDatabaseFixture Fixture { get; }
	private readonly ITestOutputHelper _output;
	public GetByIdDeletableTests(SharedDatabaseFixture fixture, ITestOutputHelper output)
		=> (Fixture, _output) = (fixture, output);

	[Fact]
	public void Delete_ExistingPlayer_Success()
	{
		Fixture.RunInTransaction(context =>
		{
			IDAOPlayer dao = new DAOPlayer(context);
			var (player, isDeletable) = dao.GetByIdDeletable("p1").Result;
			Assert.True(isDeletable);
			Assert.Equal(player, SeedData.Players[0]);

		});
	}
	[Fact]
	public void Delete_NoExistPlayer_ThrowException()
	{
		Fixture.RunInTransaction(context =>
		{
			IDAOPlayer dao = new DAOPlayer(context);
			var (player, isDeletable) = dao.GetByIdDeletable("no exist").Result;

			Assert.False(isDeletable);
			Assert.Null(player);
		});
	}
	[Fact]
	public void Delete_PlayerRegistered_ThrowException()
	{
		Fixture.RunInTransaction(context =>
		{
			IDAOPlayer dao = new DAOPlayer(context);
			var (player, isDeletable) = dao.GetByIdDeletable("p4").Result;

			Assert.False(isDeletable);
			Assert.Equal(player, SeedData.Players[3]);
		});
	}
}

