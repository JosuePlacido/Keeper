using System.Linq;
using Microsoft.EntityFrameworkCore;
using Infrastruture.Repositorys;
using Xunit;
using Domain.Models;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Test.DataExamples;

namespace Test.UnitTest.Infra.Repositorys
{
	public class PlayerSubscribeRepositoryTest : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public PlayerSubscribeRepositoryTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture,_output) = (fixture,output);

		private void Print(object item)
		{
			_output.WriteLine(JsonConvert.SerializeObject(item,Formatting.Indented));
		}

		[Fact]
		public void GetAllPlayerSubscribes()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryPlayerSubscribe(context);
				PlayerSubscribe[] items = repo.GetAll().Result;
				Assert.NotNull(items);
				Assert.NotEmpty(items);
				Assert.IsType<PlayerSubscribe[]>(items);
			}
		}
		
		[Fact]
		public void GetByValidId()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryPlayerSubscribe(context);
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
				var repo = new RepositoryPlayerSubscribe(context);
				var result = repo.GetById(id).Result;
				Assert.Null(result);
			}
		}
		[Theory]
		[ClassData(typeof(ValidPlayerSubscribeSetup))]
		public void AddValidPlayerSubscribe(PlayerSubscribe player_subscribe)
		{
			using (var context = Fixture.CreateContext())
			{
				var repoChamp = new RepositoryChampionship(context);
				var champ = ChampionshipDataExamples.GetValidBasic().First();
				repoChamp.Add(champ);
				player_subscribe.ChampionshipId = champ.Id;
				var repo = new RepositoryPlayerSubscribe(context);
				var beforeItemsCount = repo.GetAll().Result.Length;
				repo.Add(player_subscribe);
				var afterItemsCount = repo.GetAll().Result.Length;
				var items = repo.GetAll().Result;
				Assert.NotNull(player_subscribe.Id);
				Assert.True(beforeItemsCount < afterItemsCount);
			}
		}
		[Fact]
		public void UpdateValidPlayerSubscribe()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryPlayerSubscribe(context);
				var player_subscribe = repo.GetAll().Result.First();
				int old = player_subscribe.Games;
				player_subscribe.Games = -100;
				repo.Update(player_subscribe);
				Assert.NotEqual(old, player_subscribe.Games);
			}
		}
		[Theory]
		[ClassData(typeof(ValidPlayerSubscribeSetup))]
		public void RemovePlayerSubscribe(PlayerSubscribe player_subscribe)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryPlayerSubscribe(context);
				repo.Add(player_subscribe);
				var items = repo.GetAll().Result;
				repo.Remove(player_subscribe);
				var result1 = repo.GetById(player_subscribe.Id).Result;
				var count = repo.GetAll().Result.Length;
				Assert.Null(result1);
				Assert.True(items.Length > count);
			}
		}
	}
}
