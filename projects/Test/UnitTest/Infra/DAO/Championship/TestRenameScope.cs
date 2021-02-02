using System.Linq;
using Xunit;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Keeper.Infrastructure.Repository;
using Keeper.Domain.Models;
using Keeper.Application.DAO;
using Keeper.Infrastructure.DAO;
using Keeper.Domain.Enum;
using Keeper.Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace Keeper.Test.UnitTest.Infra.DAO
{
	public class TestRenameScope : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public TestRenameScope(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);

		private void Print(object item) => _output.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented,
				new JsonSerializerSettings
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore
				}));
		[Fact]
		public void TestGetChampionshipForRename()
		{
			ObjectRenameDTO result;
			Championship expected;
			using (var context = Fixture.CreateContext())
			{
				result = (ObjectRenameDTO)new DAOChampionship(context).GetByIdForRename("c1").Result;
				expected = context.Championships.AsNoTracking().Where(c => c.Id == "c1")
					.Include(c => c.Stages)
						.ThenInclude(s => ((Stage)s).Groups)
							.ThenInclude(g => ((Group)g).Matchs).FirstOrDefault();
			}
			Assert.NotNull(result);
			Assert.Equal(expected.Id, result.Id);
			Assert.Equal(expected.Name, result.Name);
			for (int s = 0; s < expected.Stages.Count; s++)
			{
				Assert.Equal(expected.Stages[s].Id, result.Childs[s].Id);
				Assert.Equal(expected.Stages[s].Name, result.Childs[s].Name);
				for (int g = 0; g < expected.Stages[s].Groups.Count; g++)
				{
					Assert.Equal(expected.Stages[s].Groups[g].Id, result.Childs[s].Childs[g].Id);
					Assert.Equal(expected.Stages[s].Groups[g].Name, result.Childs[s].Childs[g].Name);
					for (int m = 0; m < expected.Stages[s].Groups[g].Matchs.Count; m++)
					{
						Assert.Equal(expected.Stages[s].Groups[g].Matchs[m].Id,
							result.Childs[s].Childs[g].Childs[m].Id);
						Assert.Equal(expected.Stages[s].Groups[g].Matchs[m].Name,
							result.Childs[s].Childs[g].Childs[m].Name);
					}
				}
			}
		}
	}
}
