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
	public class VacancyRepositoryTest : IClassFixture<SharedDatabaseFixture>
	{
		public SharedDatabaseFixture Fixture { get; }
		private readonly ITestOutputHelper _output;
		public VacancyRepositoryTest(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (Fixture,_output) = (fixture,output);

		private void Print(object item)
		{
			_output.WriteLine(JsonConvert.SerializeObject(item,Formatting.Indented));
		}

		[Fact]
		public void GetAllVacancys()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryVacancy(context);
				Vacancy[] items = repo.GetAll().Result;
				Print(items);
				Assert.NotNull(items);
				Assert.NotEmpty(items);
				Assert.IsType<Vacancy[]>(items);
			}
		}
		
		[Fact]
		public void GetByValidId()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryVacancy(context);
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
				var repo = new RepositoryVacancy(context);
				var result = repo.GetById(id).Result;
				Assert.Null(result);
			}
		}
		[Theory]
		[ClassData(typeof(ValidVacancySetup))]
		public void AddValidVacancy(Vacancy vacancy)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryVacancy(context);
				var beforeItemsCount = repo.GetAll().Result.Length;
				repo.Add(vacancy);
				var afterItemsCount = repo.GetAll().Result.Length;
				var items = repo.GetAll().Result;
				Assert.NotNull(vacancy.Id);
				Assert.True(beforeItemsCount < afterItemsCount);
			}
		}
		[Fact]
		public void UpdateValidVacancy()
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryVacancy(context);
				var Vacancy = repo.GetAll().Result.First();
				string old = ""+Vacancy.Description;
				Vacancy.Description = "Teste";
				repo.Update(Vacancy);
				Assert.NotEqual(old, Vacancy.Description);
			}
		}
		[Theory]
		[ClassData(typeof(ValidVacancySetup))]
		public void RemoveVacancy(Vacancy vacancy)
		{
			using (var context = Fixture.CreateContext())
			{
				var repo = new RepositoryVacancy(context);
				repo.Add(vacancy);
				var items = repo.GetAll().Result;
				repo.Remove(vacancy);
				var result1 = repo.GetById(vacancy.Id).Result;
				var count = repo.GetAll().Result.Length;
				Assert.Null(result1);
				Assert.True(items.Length > count);
			}
		}
	}
}
