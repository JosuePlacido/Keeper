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
	public class TeamRepositoryTest : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public TeamRepositoryTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture,_output) = (fixture,output);

		private void Print(object item)
		{
			_output.WriteLine(JsonConvert.SerializeObject(item,Formatting.Indented));
		}

		[Fact]
		public void GetAllTeams()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryTeam(context);
				Team[] items = repo.GetAll().Result;
				Assert.NotNull(items);
				Assert.NotEmpty(items);
				Assert.IsType<Team[]>(items);
			}
		}
		
		[Fact]
		public void GetByValidId()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryTeam(context);
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
				var repo = new RepositoryTeam(context);
				var result = repo.GetById(id).Result;
				Assert.Null(result);
			}
		}
		[Theory]
		[ClassData(typeof(ValidTeamSetup))]
		public void AddValidTeam(Team Team)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryTeam(context);
				var beforeItemsCount = repo.GetAll().Result.Length;
				repo.Add(Team);
				var afterItemsCount = repo.GetAll().Result.Length;
				var items = repo.GetAll().Result;
				Assert.NotNull(Team.Id);
				Assert.True(beforeItemsCount < afterItemsCount);
			}
		}
		[Fact]
		public void UpdateValidTeam()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryTeam(context);
				var Team = repo.GetAll().Result.First();
				string old = ""+Team.Name;
				Team.Name = "Teste";
				repo.Update(Team);
				Assert.NotEqual(old, Team.Name);
			}
		}
		[Theory]
		[ClassData(typeof(ValidTeamSetup))]
		public void RemoveTeam(Team team)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryTeam(context);
				repo.Add(team);
				var items = repo.GetAll().Result;
				repo.Remove(team);
				var result1 = repo.GetById(team.Id).Result;
				var count = repo.GetAll().Result.Length;
				Assert.Null(result1);
				Assert.True(items.Length > count);
			}
		}
	}
}
