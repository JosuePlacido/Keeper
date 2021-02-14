
using System.Linq;
using Keeper.Application.Services;
using AutoMapper;
using Xunit;
using Xunit.Abstractions;
using Keeper.Infrastructure.Repository;
using Keeper.Infrastructure.DAO;
using System.Collections.Generic;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Keeper.Infrastructure.CrossCutting.Adapter;
using Newtonsoft.Json;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Keeper.Application.Contract;
using Keeper.Application.Services.EditChampionship;

namespace Keeper.Test.Integration.Application
{
	public class TestRenamesMethods : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		private readonly ApplicationContext _context;
		public SharedDatabaseFixture Fixture { get; }
		public TestRenamesMethods(SharedDatabaseFixture fixture,
			ITestOutputHelper output) => (_output, Fixture, _context) = (output, fixture, fixture.CreateContext());

		[Fact]
		public void Get_Names_Existing_Championship()
		{
			ObjectRenameDTO result;
			Championship expected;

			{
				expected = _context.Championships.AsNoTracking()
					.Where(c => c.Id == "c1").FirstOrDefault();
				var dao = new DAOChampionship(_context);
				result = new EditChampionshipService(null, new UnitOfWork(_context, null))
					.GetNames("c1").Result;
			}
			Assert.NotNull(result);
			Assert.Equal(expected.Id, result.Id);
			Assert.Equal(expected.Name, result.Name);
		}
		[Fact]
		public void Get_Names_No_Existing_Championship()
		{
			ObjectRenameDTO result;

			{
				var dao = new DAOChampionship(_context);
				result = new EditChampionshipService(null, new UnitOfWork(_context, null))
					.GetNames("no exist").Result;
			}
			Assert.Null(result);
		}
		[Fact]
		public void RenamesChampionship()
		{
			Championship result;

			{
				var dao = new DAOChampionship(_context);
				var service = new EditChampionshipService(null, new UnitOfWork(_context, null));
				var test = service.GetNames("c1").Result;
				test.Name = test.Name + " edit";
				foreach (var stage in test.Childs)
				{
					stage.Name = (stage.Name + " edit");
					foreach (var group in stage.Childs)
					{
						group.Name = (group.Name + " edit");
						foreach (var match in group.Childs)
						{
							match.Name = (match.Name + " edit");
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
		}
		[Fact]
		public void RenamesNullChampionship()
		{
			IServiceResponse result;

			{
				var dao = new DAOChampionship(_context);
				var service = new EditChampionshipService(null, new UnitOfWork(_context, null));
				result = service.RenameScopes(null).Result;
			}
			Assert.Null(result.Value);
			Assert.False(result.ValidationResult.IsValid);
		}
	}
}
