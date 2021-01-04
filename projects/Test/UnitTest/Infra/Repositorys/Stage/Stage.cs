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
	public class StageRepositoryTest : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public StageRepositoryTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture,_output) = (fixture,output);

		private void Print(object item)
		{
			_output.WriteLine(JsonConvert.SerializeObject(item,Formatting.Indented));
		}

		[Fact]
		public void GetAllStages()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryStage(context);
				Stage[] items = repo.GetAll().Result;
				Assert.NotNull(items);
				Assert.NotEmpty(items);
				Assert.IsType<Stage[]>(items);
			}
		}
		
		[Fact]
		public void GetByValidId()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryStage(context);
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
				var repo = new RepositoryStage(context);
				var result = repo.GetById(id).Result;
				Assert.Null(result);
			}
		}
		[Theory]
		[ClassData(typeof(ValidStageSetup))]
		public void AddValidStage(Stage stage)
		{
			using (var context = Fixture.CreateContext())
			{
				var repoChamp = new RepositoryChampionship(context);
				stage.ChampionshipId = repoChamp.GetAll().Result.First().Id;
				var repo = new RepositoryStage(context);
				var beforeItemsCount = repo.GetAll().Result.Length;
				repo.Add(stage);
				var afterItemsCount = repo.GetAll().Result.Length;
				var items = repo.GetAll().Result;
				Assert.NotNull(stage.Id);
				Assert.True(beforeItemsCount < afterItemsCount);
			}
		}
		[Fact]
		public void UpdateValidStage()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryStage(context);
				var stage = repo.GetAll().Result.First();
				string old = ""+ stage.Name;
				stage.Name = "Teste";
				repo.Update(stage);
				Assert.NotEqual(old, stage.Name);
			}
		}
		[Theory]
		[ClassData(typeof(ValidStageSetup))]
		public void RemoveStage(Stage stage)
		{
			using (var context = Fixture.CreateContext())
			{
				var repoChamp = new RepositoryChampionship(context);
				stage.ChampionshipId = repoChamp.GetAll().Result.First().Id;
				var repo = new RepositoryStage(context);
				repo.Add(stage);
				var items = repo.GetAll().Result;
				repo.Remove(stage);
				var result1 = repo.GetById(stage.Id).Result;
				var count = repo.GetAll().Result.Length;
				Assert.Null(result1);
				Assert.True(items.Length > count);
			}
		}
	}
}
