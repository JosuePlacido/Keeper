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
	public class PlayerRepositoryTest : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public PlayerRepositoryTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture,_output) = (fixture,output);

		private void Print(object item)
		{
			_output.WriteLine(JsonConvert.SerializeObject(item,Formatting.Indented));
		}

		[Fact]
		public void GetAllPlayers()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryPlayer(context);
				Player[] items = repo.GetAll().Result;
				Assert.NotNull(items);
				Assert.NotEmpty(items);
				Assert.IsType<Player[]>(items);
			}
		}
		
		[Fact]
		public void GetByValidId()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryPlayer(context);
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
				var repo = new RepositoryPlayer(context);
				var result = repo.GetById(id).Result;
				Assert.Null(result);
			}
		}
		[Theory]
		[ClassData(typeof(ValidPlayerSetup))]
		public void AddValidPlayer(Player player)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryPlayer(context);
				var beforeItemsCount = repo.GetAll().Result.Length;
				repo.Add(player);
				var afterItemsCount = repo.GetAll().Result.Length;
				var items = repo.GetAll().Result;
				Assert.NotNull(player.Id);
				Assert.True(beforeItemsCount < afterItemsCount);
			}
		}
		[Fact]
		public void UpdateValidPlayer()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryPlayer(context);
				var player = repo.GetAll().Result.First();
				string old = ""+player.Name;
				player.Name = "Teste";
				repo.Update(player);
				Assert.NotEqual(old, player.Name);
			}
		}
		[Theory]
		[ClassData(typeof(ValidPlayerSetup))]
		public void RemovePlayer(Player player)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryPlayer(context);
				repo.Add(player);
				var items = repo.GetAll().Result;
				repo.Remove(player);
				var result1 = repo.GetById(player.Id).Result;
				var count = repo.GetAll().Result.Length;
				Assert.Null(result1);
				Assert.True(items.Length > count);
			}
		}
	}
}
