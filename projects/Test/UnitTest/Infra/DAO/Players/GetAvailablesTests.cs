using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Domain.Models;
using Infrastructure.DAO;
using Application.DTO;

namespace Test.UnitTest.Infra.DAO.Players;

public class TestGetAvailables : IClassFixture<SharedDatabaseFixture>
{
	public SharedDatabaseFixture Fixture { get; }
	private readonly ITestOutputHelper _output;
	public TestGetAvailables(SharedDatabaseFixture fixture, ITestOutputHelper output)
		=> (Fixture, _output) = (fixture, output);

	[Theory]
	[ClassData(typeof(GetAvailablesSetup))]
	public void TestGetPlayersWithFreeAgent(PaginationDTO<Player> expected,
		string championshipId, string terms)
	{
		Fixture.RunInTransaction(context =>
		{
			PaginationDTO<Player> result;
			var dao = new DAOPlayer(context);
			result = dao.GetAvailables(terms, championshipId, expected.Page, expected.Take).Result;

			Assert.Equal(expected.Total, result.Total);
			Assert.Equal(expected.Items, result.Items);
		});
	}
}
internal class GetAvailablesSetup : TheoryData<PaginationDTO<Player>, string, string>
{
	public GetAvailablesSetup()
	{
		Player[] players = SeedData.Players.OrderBy(p => p.Name).ToArray();
		// Get non sub and 1 freeAgent
		Add(new PaginationDTO<Player>
		{
			Take = 10,
			Page = 1,
			Items = players.Where((p, i) => new string[] { "p1", "p2", "p3", "p4", "p11" }.Contains(p.Id)).ToArray(),
			Total = 5,
		}, "c1", "");
		// Get freeAgent filtered
		Add(new PaginationDTO<Player>
		{
			Take = 10,
			Page = 1,
			Items = players.Where((p, i) => p.Id == "p4").ToArray(),
			Total = 1
		}, "c1", "player4");
		// Get All Page 1
		Add(new PaginationDTO<Player>
		{
			Take = 10,
			Page = 1,
			Items = players.Take(10).ToArray(),
			Total = 11
		}, "c2", "");
		// Get All Page 2
		Add(new PaginationDTO<Player>
		{
			Take = 10,
			Page = 2,
			Items = players.Skip(10).ToArray(),
			Total = 11
		}, "c2", "");
		// Get All Filtered
		Add(new PaginationDTO<Player>
		{
			Take = 10,
			Page = 1,
			Items = players.Where(p => new string[] { "p1", "p2", "p3", "p4" }.Contains(p.Id)).ToArray(),
			Total = 4
		}, "c2", "player");
		// Get Empty List
		Add(new PaginationDTO<Player>
		{
			Take = 10,
			Page = 1,
			Items = new Player[0],
			Total = 0
		}, "c1", "no one");
	}
}

