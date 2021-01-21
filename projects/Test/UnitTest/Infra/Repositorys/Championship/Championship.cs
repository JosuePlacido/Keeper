using System.Linq;
using Xunit;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Keeper.Infrastructure.Repository;
using Keeper.Domain.Models;

namespace Keeper.Test.UnitTest.Infra.Repositorys
{
	public class ChampionshipRepositoryTest : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public ChampionshipRepositoryTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);

		private void Print(object item) => _output.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented,
				new JsonSerializerSettings
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore
				}));

		[Fact]
		public void GetAllChampionships()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new ChampionshipRepository(context);
				Championship[] items = repo.GetAll().Result;
				Assert.NotNull(items);
				Assert.NotEmpty(items);
				Assert.IsType<Championship[]>(items);
			}
		}

		[Fact]
		public void GetByValidId()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new ChampionshipRepository(context);
				var item = repo.GetAll().Result.Last();
				var result = repo.GetById(item.Id).Result;
				Assert.Equal(result, item);
			}
		}
		[Theory]
		[InlineData("")]
		[InlineData(null)]
		[InlineData("teste")]
		public void GetByInvalidId(string id)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new ChampionshipRepository(context);
				var result = repo.GetById(id).Result;
				Assert.Null(result);
			}
		}
		[Theory]
		[ClassData(typeof(ValidChampionshipSetup))]
		public async void AddValidChampionship(Championship Championship)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new ChampionshipRepository(context);
				var beforeItemsCount = repo.GetAll().Result.Length;
				await repo.Add(Championship);
				var afterItemsCount = repo.GetAll().Result.Length;
				var items = repo.GetAll().Result;
				Assert.NotNull(Championship.Id);
				Assert.True(beforeItemsCount < afterItemsCount);
			}
		}
		[Fact]
		public async void UpdateValidChampionship()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new ChampionshipRepository(context);
				var Championship = repo.GetAll().Result.First();
				string old = "" + Championship.Name;
				Championship.EditScope("UPDATE");
				await repo.Update(Championship);
				Assert.NotEqual(old, Championship.Name);
			}
		}
		[Fact]
		public async void RemoveChampionship()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new ChampionshipRepository(context);
				var championship = repo.GetAll().Result.Where(c => c.Name == "remove" || c.Name == "UPDATE").FirstOrDefault();
				await repo.Remove(championship);
				var result1 = repo.GetById(championship.Id).Result;
				var count = repo.GetAll().Result.Length;
				Assert.Null(result1);
				Assert.DoesNotContain(championship, repo.GetAll().Result);
			}
		}
	}
}
