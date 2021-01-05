using System.Linq;
using Microsoft.EntityFrameworkCore;
using Infrastruture.Repositorys;
using Xunit;
using Domain.Models;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Test.UnitTest.Infra.Repositorys
{
	public class MatchRepositoryTest : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public MatchRepositoryTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture, _output) = (fixture, output);

		private void Print(object item)
		{
			_output.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));
		}

		[Fact]
		public void GetAllMatchs()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryMatch(context);
				Match[] items = repo.GetAll().Result;
				Assert.NotNull(items);
				Assert.NotEmpty(items);
				Assert.IsType<Match[]>(items);
			}
		}

		[Fact]
		public void GetByValidId()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryMatch(context);
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
				var repo = new RepositoryMatch(context);
				var result = repo.GetById(id).Result;
				Assert.Null(result);
			}
		}
		[Theory]
		[ClassData(typeof(ValidMatchSetup))]
		public void AddValidMatch(Match match)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryMatch(context);
				var beforeItemsCount = repo.GetAll().Result.Length;
				repo.Add(match);
				var afterItemsCount = repo.GetAll().Result.Length;
				var items = repo.GetAll().Result;
				Assert.NotNull(match.Id);
				Assert.True(beforeItemsCount < afterItemsCount);
			}
		}
		[Fact]
		public void UpdateValidMatch()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryMatch(context);
				var Match = repo.GetAll().Result.First();
				string old = "" + Match.Name;
				Match.EditMatchDetails(name: "Teste");
				repo.Update(Match);
				Assert.NotEqual(old, Match.Name);
			}
		}
		[Theory]
		[ClassData(typeof(ValidMatchSetup))]
		public void RemoveMatch(Match match)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryMatch(context);
				var items = repo.GetAll().Result;
				repo.Remove(match);
				var result1 = repo.GetById(match.Id).Result;
				var count = repo.GetAll().Result.Length;
				Assert.Null(result1);
				Assert.True(items.Length > count);
			}
		}
	}
}
