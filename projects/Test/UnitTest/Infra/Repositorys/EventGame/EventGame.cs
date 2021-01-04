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
	public class EventGameRepositoryTest : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public EventGameRepositoryTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture,_output) = (fixture,output);

		private void Print(object item)
		{
			_output.WriteLine(JsonConvert.SerializeObject(item,Formatting.Indented,
				new JsonSerializerSettings
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore
				}));
		}

		[Fact]
		public void GetAllEventGames()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryEventGame(context);
				EventGame[] items = repo.GetAll().Result;
				Assert.NotNull(items);
				Assert.NotEmpty(items);
				Assert.IsType<EventGame[]>(items);
			}
		}
		
		[Fact]
		public void GetByValidId()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryEventGame(context);
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
				var repo = new RepositoryEventGame(context);
				var result = repo.GetById(id).Result;
				Assert.Null(result);
			}
		}
		[Theory]
		[ClassData(typeof(ValidEventGameSetup))]
		public void AddValidEventGame(EventGame EventGame)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryEventGame(context);
				var beforeItemsCount = repo.GetAll().Result.Length;
				repo.Add(EventGame);
				var afterItemsCount = repo.GetAll().Result.Length;
				var items = repo.GetAll().Result;
				Assert.NotNull(EventGame.Id);
				Assert.True(beforeItemsCount < afterItemsCount);
			}
		}
		[Fact]
		public void UpdateValidEventGame()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryEventGame(context);
				var EventGame = repo.GetAll().Result.First();
				string old = ""+EventGame.Description;
				EventGame.Description = "Teste";
				repo.Update(EventGame);
				Assert.NotEqual(old, EventGame.Description);
			}
		}
		[Theory]
		[ClassData(typeof(ValidEventGameSetup))]
		public void RemoveEventGame(EventGame championship)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryEventGame(context);
				repo.Add(championship);
				var items = repo.GetAll().Result;
				repo.Remove(championship);
				var result1 = repo.GetById(championship.Id).Result;
				var count = repo.GetAll().Result.Length;
				Assert.Null(result1);
				Assert.True(items.Length > count);
			}
		}
	}
}
