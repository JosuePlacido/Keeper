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
	public class GroupRepositoryTest : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public GroupRepositoryTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture,_output) = (fixture,output);

		private void Print(object item)
		{
			_output.WriteLine(JsonConvert.SerializeObject(item,Formatting.Indented));
		}

		[Fact]
		public void GetAllGroups()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryGroup(context);
				Group[] items = repo.GetAll().Result;
				Assert.NotNull(items);
				Assert.NotEmpty(items);
				Assert.IsType<Group[]>(items);
			}
		}
		
		[Fact]
		public void GetByValidId()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryGroup(context);
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
				var repo = new RepositoryGroup(context);
				var result = repo.GetById(id).Result;
				Assert.Null(result);
			}
		}
		[Theory]
		[ClassData(typeof(ValidGroupSetup))]
		public void AddValidGroup(Group group)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryGroup(context);
				var beforeItemsCount = repo.GetAll().Result.Length;
				repo.Add(group);
				var afterItemsCount = repo.GetAll().Result.Length;
				var items = repo.GetAll().Result;
				Assert.NotNull(group.Id);
				Assert.True(beforeItemsCount < afterItemsCount);
			}
		}
		[Fact]
		public void UpdateValidGroup()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryGroup(context);
				var group = repo.GetAll().Result.First();
				string old = ""+ group.Name;
				group.Name = "Teste";
				repo.Update(group);
				Assert.NotEqual(old, group.Name);
			}
		}
		[Theory]
		[ClassData(typeof(ValidGroupSetup))]
		public void RemoveGroup(Group group)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryGroup(context);
				repo.Add(group);
				var items = repo.GetAll().Result;
				repo.Remove(group);
				var result1 = repo.GetById(group.Id).Result;
				var count = repo.GetAll().Result.Length;
				Assert.Null(result1);
				Assert.True(items.Length > count);
			}
		}
	}
}
