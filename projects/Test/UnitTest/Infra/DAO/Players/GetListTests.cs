using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Domain.Models;
using Infrastructure.DAO;
using Application.DTO;

namespace Test.UnitTest.Infra.DAO.Players;

public class ListFilteredPaginationTests : IClassFixture<SharedDatabaseFixture>
{
	public SharedDatabaseFixture Fixture { get; }
	private readonly ITestOutputHelper _output;
	public ListFilteredPaginationTests(SharedDatabaseFixture fixture, ITestOutputHelper output)
		=> (Fixture, _output) = (fixture, output);

	[Theory]
	[ClassData(typeof(ListFilteredPaginationSetup))]
	public void TestGetPlayersWithFreeAgent(PaginationDTO<Player> expected, string terms)
	{
		Fixture.RunInTransaction(context =>
		{
			PaginationDTO<Player> result;
			var dao = new DAOPlayer(context);
			result = dao.List(terms, expected.Page, expected.Take).Result;

			Assert.Equal(expected.Total, result.Total);
			Assert.Equal(expected.Items, result.Items);
			Assert.Equal(expected.Page, result.Page);
			Assert.Equal(expected.Take, result.Take);
		});
	}
}
internal class ListFilteredPaginationSetup : TheoryData<PaginationDTO<Player>, string>
{
	public ListFilteredPaginationSetup()
	{
		Player[] players = SeedData.Players.OrderBy(p => p.Name).ToArray();
		// Get empty list
		Add(new PaginationDTO<Player>
		{
			Take = 10,
			Page = 1,
			Items = new Player[0],
			Total = 0,
		}, "no existing player");
		// get full players page 1 take 5
		Add(new PaginationDTO<Player>
		{
			Take = 5,
			Page = 1,
			Items = players.Take(5).ToArray(),
			Total = 11
		}, "");
		// get full players page 2 take 5
		Add(new PaginationDTO<Player>
		{
			Take = 5,
			Page = 2,
			Items = players.Skip(5).Take(5).ToArray(),
			Total = 11
		}, "");
		// get full players page 3 take 5
		Add(new PaginationDTO<Player>
		{
			Take = 5,
			Page = 3,
			Items = players.Skip(10).ToArray(),
			Total = 11
		}, "");
		// Get All Page 2
		Add(new PaginationDTO<Player>
		{
			Take = 10,
			Page = 2,
			Items = players.Skip(10).ToArray(),
			Total = 11
		}, "");
		// Get filtered
		Add(new PaginationDTO<Player>
		{
			Take = 10,
			Page = 1,
			Items = players.Where(p => new string[] { "p1", "p2", "p3", "p4" }.Contains(p.Id)).ToArray(),
			Total = 4
		}, "player");
		// Get filtered with normalizzedTerms
		Add(new PaginationDTO<Player>
		{
			Take = 10,
			Page = 1,
			Items = players.Where(p => new string[] { "p1", "p2", "p3", "p4" }.Contains(p.Id)).ToArray(),
			Total = 4
		}, "pLAYÉr");
	}
}

