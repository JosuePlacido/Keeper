
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.DAO;
using System.Collections.Generic;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Services.EditChampionship;
using Application.Contract;

namespace Test.Integration.Application
{
	public class TestRenamesMethods : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		public SharedDatabaseFixture Fixture { get; }
		public TestRenamesMethods(SharedDatabaseFixture fixture,
			ITestOutputHelper output) => (_output, Fixture) = (output, fixture);

		[Fact]
		public void Get_Names_Existing_Championship()
		{
			ObjectRenameDTO result;
			Championship expected;

			Fixture.RunInTransaction(context =>
			{
				{
					expected = context.Championships.AsNoTracking()
						.Where(c => c.Id == "c1").FirstOrDefault();
					var dao = new DAOChampionship(context);
					result = new EditChampionshipService(null, new UnitOfWork(context, null))
						.GetNames("c1").Result;
				}
				Assert.NotNull(result);
				Assert.Equal(expected.Id, result.Id);
				Assert.Equal(expected.Name, result.Name);
			});
		}
		[Fact]
		public void Get_Names_No_Existing_Championship()
		{
			ObjectRenameDTO result;

			Fixture.RunInTransaction(context =>
			{
				{
					var dao = new DAOChampionship(context);
					result = new EditChampionshipService(null, new UnitOfWork(context, null))
						.GetNames("no exist").Result;
				}
				Assert.Null(result);
			});
		}
		[Fact]
		public void RenamesChampionship()
		{
			Championship result;

			Fixture.RunInTransaction(context =>
			{
				{
					var dao = new DAOChampionship(context);
					var service = new EditChampionshipService(null, new UnitOfWork(context, null));
					var test = service.GetNames("c1").Result;
					test.Name += " edit";
					foreach (var stage in test.Childs)
					{
						stage.Name += " edit";
						foreach (var group in stage.Childs)
						{
							group.Name += " edit";
							foreach (var match in group.Childs)
							{
								match.Name += " edit";
							}
						}
					}
					result = (Championship)service.RenameScopes(test).Result.Value;
				}
				List<string> newNames = new List<string>();
				Assert.NotNull(result);
				newNames.Add(result.Name);
				newNames.AddRange(result.Stages.Select(s => s.Name));
				newNames.AddRange(result.Stages.SelectMany(s => s.Groups.Select(g => g.Name)));
				newNames.AddRange(result.Stages.SelectMany(s => s.Groups
					.SelectMany(g => g.Matchs.Select(m => m.Name))));
				Assert.All(newNames, i => i.Contains(" edit"));
			});
		}
		[Fact]
		public void RenamesNullChampionship()
		{
			Fixture.RunInTransaction(context =>
			{
				IServiceResponse result;

				{
					var dao = new DAOChampionship(context);
					var service = new EditChampionshipService(null, new UnitOfWork(context, null));
					result = service.RenameScopes(null).Result;
				}
				Assert.Null(result.Value);
				Assert.False(result.ValidationResult.IsValid);
			});
		}
	}
}
