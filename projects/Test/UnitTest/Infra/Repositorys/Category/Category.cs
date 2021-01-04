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
	public class CategoryRepositoryTest : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public CategoryRepositoryTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture,_output) = (fixture,output);

		private void Print(object item)
		{
			_output.WriteLine(JsonConvert.SerializeObject(item,Formatting.Indented));
		}

		[Fact]
		public void GetAllCategorys()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryCategory(context);
				Category[] items = repo.GetAll().Result;
				Assert.NotNull(items);
				Assert.NotEmpty(items);
				Assert.IsType<Category[]>(items);
			}
		}
		
		[Fact]
		public void GetByValidId()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryCategory(context);
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
				var repo = new RepositoryCategory(context);
				var result = repo.GetById(id).Result;
				Assert.Null(result);
			}
		}
		[Theory]
		[ClassData(typeof(ValidCategorySetup))]
		public void AddValidCategory(Category Category)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryCategory(context);
				var beforeItemsCount = repo.GetAll().Result.Length;
				repo.Add(Category);
				var afterItemsCount = repo.GetAll().Result.Length;
				var items = repo.GetAll().Result;
				Assert.NotNull(Category.Id);
				Assert.True(beforeItemsCount < afterItemsCount);
			}
		}
		[Fact]
		public void UpdateValidCategory()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryCategory(context);
				var Category = repo.GetAll().Result.First();
				string old = ""+Category.Name;
				Category.Name = "Teste";
				repo.Update(Category);
				Assert.NotEqual(old, Category.Name);
			}
		}
		[Theory]
		[ClassData(typeof(ValidCategorySetup))]
		public void RemoveCategory(Category category)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryCategory(context);
				repo.Add(category);
				var items = repo.GetAll().Result;
				repo.Remove(category);
				var result1 = repo.GetById(category.Id).Result;
				var count = repo.GetAll().Result.Length;
				Assert.Null(result1);
				Assert.True(items.Length > count);
			}
		}
	}
}
