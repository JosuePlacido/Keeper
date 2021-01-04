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
	public class StatisticsRepositoryTest : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public StatisticsRepositoryTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture,_output) = (fixture,output);

		private void Print(object item)
		{
			_output.WriteLine(JsonConvert.SerializeObject(item,Formatting.Indented));
		}

		[Fact]
		public void GetAllStatisticss()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryStatistics(context);
				Statistics[] items = repo.GetAll().Result;
				Assert.NotNull(items);
				Assert.NotEmpty(items);
				Assert.IsType<Statistics[]>(items);
			}
		}
		
		[Fact]
		public void GetByValidId()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryStatistics(context);
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
				var repo = new RepositoryStatistics(context);
				var result = repo.GetById(id).Result;
				Assert.Null(result);
			}
		}
		[Theory]
		[ClassData(typeof(ValidStatisticsSetup))]
		public void AddValidStatistics(Statistics statistics)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryStatistics(context);
				var beforeItemsCount = repo.GetAll().Result.Length;
				repo.Add(statistics);
				var afterItemsCount = repo.GetAll().Result.Length;
				var items = repo.GetAll().Result;
				Assert.NotNull(statistics.Id);
				Assert.True(beforeItemsCount < afterItemsCount);
			}
		}
		[Fact]
		public void UpdateValidStatistics()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryStatistics(context);
				var Statistics = repo.GetAll().Result.First();
				string old = ""+Statistics.Lastfive;
				Statistics.Lastfive = "Teste update";
				repo.Update(Statistics);
				Assert.NotEqual(old, Statistics.Lastfive);
			}
		}
		[Theory]
		[ClassData(typeof(ValidStatisticsSetup))]
		public void RemoveStatistics(Statistics statistics)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryStatistics(context);
				repo.Add(statistics);
				var items = repo.GetAll().Result;
				repo.Remove(statistics);
				var result1 = repo.GetById(statistics.Id).Result;
				var count = repo.GetAll().Result.Length;
				Assert.Null(result1);
				Assert.True(items.Length > count);
			}
		}
	}
}
