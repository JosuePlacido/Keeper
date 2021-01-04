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
	public class TeamSubscribeRepositoryTest : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public TeamSubscribeRepositoryTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture,_output) = (fixture,output);

		private void Print(object item)
		{
			_output.WriteLine(JsonConvert.SerializeObject(item,Formatting.Indented));
		}

		[Fact]
		public void GetAllTeamSubscribes()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryTeamSubscribe(context);
				TeamSubscribe[] items = repo.GetAll().Result;
				Assert.NotNull(items);
				Assert.NotEmpty(items);
				Assert.IsType<TeamSubscribe[]>(items);
			}
		}
		
		[Fact]
		public void GetByValidId()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryTeamSubscribe(context);
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
				var repo = new RepositoryTeamSubscribe(context);
				var result = repo.GetById(id).Result;
				Assert.Null(result);
			}
		}
		[Theory]
		[ClassData(typeof(ValidTeamSubscribeSetup))]
		public void AddValidTeamSubscribe(TeamSubscribe team_subscribe)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryTeamSubscribe(context);
				var beforeItemsCount = repo.GetAll().Result.Length;
				repo.Add(team_subscribe);
				var afterItemsCount = repo.GetAll().Result.Length;
				var items = repo.GetAll().Result;
				Assert.NotNull(team_subscribe.Id);
				Assert.True(beforeItemsCount < afterItemsCount);
			}
		}
		[Fact]
		public void UpdateValidTeamSubscribe()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryTeamSubscribe(context);
				var team_subscribe = repo.GetAll().Result.First();
				string old = ""+ team_subscribe.Status;
				team_subscribe.Status = "Teste Update";
				repo.Update(team_subscribe);
				Assert.NotEqual(old, team_subscribe.Status);
			}
		}
		[Theory]
		[ClassData(typeof(ValidTeamSubscribeSetup))]
		public void RemoveTeamSubscribe(TeamSubscribe team_subscribe)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryTeamSubscribe(context);
				repo.Add(team_subscribe);
				var items = repo.GetAll().Result;
				repo.Remove(team_subscribe);
				var result1 = repo.GetById(team_subscribe.Id).Result;
				var count = repo.GetAll().Result.Length;
				Assert.Null(result1);
				Assert.True(items.Length > count);
			}
		}
	}
}
