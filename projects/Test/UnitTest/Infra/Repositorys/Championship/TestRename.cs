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
	public class TestRenamechampionship : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public TestRenamechampionship(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);

		private void Print(object item) => _output.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented,
				new JsonSerializerSettings
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore
				}));

		[Fact]
		public void RenameChampionship()
		{
			Championship expected;
			List<string> newNames = new List<string>();
			using (var context = Fixture.CreateContext())
			{
				expected = context.Championships.AsNoTracking().Where(c => c.Id == "c1")
					.Include(c => c.Stages)
						.ThenInclude(s => ((Stage)s).Groups)
							.ThenInclude(g => ((Group)g).Matchs).FirstOrDefault();
				expected.EditScope(expected.Name + " edit");
				foreach (var stage in expected.Stages)
				{
					stage.EditScope(stage.Name + " edit");
					foreach (var group in stage.Groups)
					{
						group.EditScope(group.Name + " edit");
						foreach (var match in group.Matchs)
						{
							match.EditScope(match.Name + " edit");
						}
					}
				}
				expected = new ChampionshipRepository(context).RenameScopes(expected).Result;
				newNames.Add(expected.Name);
				newNames.AddRange(expected.Stages.Select(s => s.Name));
				newNames.AddRange(expected.Stages.SelectMany(s => s.Groups.Select(g => g.Name)));
				newNames.AddRange(expected.Stages.SelectMany(s => s.Groups
					.SelectMany(g => g.Matchs.Select(m => m.Name))));
			}
			Assert.All(newNames, i => i.Contains(" edit"));
		}
	}
}
